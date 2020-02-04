/**
** 2D Graphical Editor (2d-gx)
**
** gxLine.c
*/
#include <stdio.h>
#include <X11/Intrinsic.h>
#include "gxGraphics.h"
static void GXRubberLine( GXLine *line )
{
    int indx;
    if( line && line->pts && (line->num_pts > 1)) {

        indx = line->num_pts - 1;

        XDrawLine( XtDisplay(GxDrawArea),
        XtWindow(GxDrawArea) , rubberGC,
        line->pts[indx ].x,
        line->pts[indx ].y,
        line->pts[indx-1].x,
        line->pts[indx-1].y );

    }
}

static void create_line( GXObjPtr _obj, GXLine *line )
{
    GXLinePtr line_data;
    GXObjPtr obj = _obj;

    if( obj == NULL ) {
        obj = gx_create_obj();
    }

    line_data = (GXLinePtr) XtNew(GxXLine) ;

    memcpy( (char *)line_data, (char *)line, sizeof (GXLine));
    obj->data = line_data;
    obj ->draw = line_draw;
    obj ->erase = line_erase;
    obj ->find = line_find;
    obj ->move = line_move;
    obj ->scale = line_scale;
    obj - >copy = line_copy;
    obj->select = line_select;
    obj->deselect = line_deselect;

    obj->save = line_save;

    gx_add_obj( obj );
}

static GXLinePtr update_line( XEvent *event, GXLine *line )
{
    static GxXLine xline;
    GXLinePtr xlinePtr = &xline;

    if( line == NULL ) {
        xline.pts = (XPoint *)XtMalloc( sizeof (XPoint) );
        xline.num_pts = 0;
    } else {
        xline.pts = (XPoint *)XtRealloc( (char *)xline.pts,
        sizeof(XPoint) * (xline.num_pts + 1));
    }
    xline.pts[xline.num_pts].x = event->xbutton.x;
    xline.pts[xline.num_pts].y = event->xbutton.y;
    xline.num_pts++;

    return xlinePtr;
}

static void draw_erase( GXObjPtr line, Boolean tile )
{
    GC gc;
    GXLinePtr line_data = (GXLinePtr)line->data;

    gc = gx_allocate_gc( line, tile );

    XDrawLines(XtDisplay(GxDrawArea),
            XtWindow(GxDrawArea), gc, line_data->pts, 
            line_data->num_pts, CoordModeOrigin );

    XtReleaseGC( GxDrawArea, gc );
}
static void line_draw( GXObjPtr line )
{
    draw_erase( line, False );
}
static void line_erase( GXObjPtr line )
{
    draw_erase( line, True );
}

static Boolean line_find(GxXObjPtr line, XEvent *event)
{
    Boolean found = False;
    XPoint p;

    p.x = event->xbutton.x;
    p.y = event->xbutton.y;

    found = point_selected( (GXLinePtr)line->data, &p );
    if( found == False )
        found = segment_selected( (GXLinePtr)line->data, &p) ;

    return found;
}

static Boolean point_equal_event( GXLine *line, XEvent *event )
{
    Boolean pts_equal = False;
    int xe_x, xe_y;
    xe_x = event->xbutton.x;
    xe_y = event->xbutton.y;
    /*
    * the last point will always be the current motion event
    * so check the one before for redundancy (equates to a
    * double click to end the action )
    */
    if( line && (line->num_pts > 2) ) {
        int num = line->num_pts - 2;
        if( (abs(line->pts[num].x - xe_x) <= TOLERANCE ) && (abs(line->pts[num].y - xe_y) <= TOLERANCE )) {
            pts_equal = True;
            }
    }
    return pts_equal;
}

static Boolean segment_selected( GXLinePtr data, XPoint *pt )
{
    Boolean found = False;
    int i;

    for(i=0; i<data->num_pts-1 && found == False; i++) {
        found = near_segment(data->pts[i].x,data->pts[i].y,
            data->pts[i + 1].x, data->pts[i + 1].y,
            pt->x, pt->y );
    }
    return found;
}
static Boolean point_selected( GXLinePtr line, XPoint *pt )
    {
    int i, x, y, found = False;
    for( i = 0; (i < line->num_pts) && ! found; i++ ) {

        x = line->pts[i].x;
        y = line->pts[i].y;
        
        if( abs(pt->x - x) <= TOLERANCE && abs(pt->y - y) <= TOLERANCE ) {
            found = True;
        }

    }
    return found;
}

static void line_select( GXObjPtr line ){
    line_bounding_handles( line );
    gx_draw_handles( line );
}



void gx_line( XEvent *event )
{

    static GXLine *rubber_line = NULL;
    if( event == NULL ) {
        rubber_line = NULL;
    } else {
                /* remove the current rubber line (if there is one) */
                GXRubberLine( rubber_line );
            switch( event->type ) {
            case ButtonPress:
                if( rubber_line == NULL ) set_cursor( LINE_MODE );
                /*
                * See if we ‘double clicked’ & are done selecting points
                */
                if( point_equal_event( rubber_line, event ) == True ) {

                    /* erase our temp line */

                    XDrawLines( XtDisplay(GxDrawArea), XtWindow(GxDrawArea) ,
                    rubberGC, rubber_line->pts,
                    rubber_line->num_pts, CoordModeOrigin );

                    create_line( NULL, rubber_line );

                    gx_refresh();

                    set_cursor( NORMAL_MODE );

                    rubber_line = NULL;

                } else {
                /*
                    * Initialize a GXLine struture to manage our creationGxXRubberLine
                    */
                    GxXRubberLine( rubber_line );
                    rubber_line = update_line( event, rubber_line );
                }
            break; 
            case ButtonRelease:
            
            case MotionNotify:
                /*
                * update the GXLine structure based on the
                * new point and current location of the mouse
                */
                if( rubber_line ) {
                    /*
                    * replace the last point with the current event location
                    * IF we have more than one point
                    */
                    if( rubber_line->num_pts > 1 ) {
                        rubber_line->pts[rubber_line->num_pts-1].x = event ->xbutton.x;
                        rubber_line->pts[rubber_line->num_pts-1].y = event ->xbutton.y;
                    } else {
                        (void)update_line( event, rubber_line);
                    }
                    /*
                    * redraw the rubberbanding line
                    */
                    GxXRubberLine( rubber_line );
                }
            break;
            }
        }

}

void gx_pencil( XEvent *event )
{
    printf( “draw freestlye\n” );
}
void gx_arrow( XEvent *event )
{
    printf( “draw an arrow\n” );
}
void gx_box( XEvent *event )
{
    printf( “draw a box\n” );
}

/**
** end of gxLine.c
*/