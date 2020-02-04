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

static Boolean segment_selected( GXLinePtr data, XPoint *pt ){
    Boolean found = False;
    int i;

    for(i=0; i<data->num_pts-1 && found == False; i++) {
        found = near_segment(data->pts[i].x,data->pts[i].y,
            data->pts[i + 1].x, data->pts[i + 1].y,
            pt->x, pt->y );
    }
    return found;
}
static Boolean point_selected( GXLinePtr line, XPoint *pt )  {
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

static void line_bounding_handles( GXObjPtr gx_line ){
    int i, x1, y1, x2, y2, width, height;

    GXLine *line_data = gx_line->data;

    gx_line->handles =
    (XRectangle *)XtMalloc( sizeof(XRectangle) * 8 );

    gx_line->num_handles = 8;
    if( gx_line->handles == NULL ) {
        perror( "Alloc failed for line handles" );
        gx_line->num_handles = 0;
    return;
    }
    for( i= 0; i < 8; i++ ) {
        gx_line->handles[i].width = HNDL_SIZE;
        gx_line->handles[i].height = HNDL_SIZE;
    }

    get_bounds( line_data->pts, line_data->num_pts,
    &x1, &y1, &x2, &y2 );

    width = x2 - x1;
    height = y2 - y1;

    gx_line->handles[0].x = x1 - HNDL_OFFSET - HNDL_SIZE;
    gx_line->handles[0].y = y1 - HNDL_OFFSET - HNDL_SIZE;
    
    gx_line->handles[1].x = x1 + (width/2) - HNDL_OFFSET;
    gx_line->handles[1].y = y1 - HNDL_SIZE - HNDL_OFFSET;
    
    gx_line->handles[2].x = x2 + HNDL_OFFSET;
    gx_line->handles[2].y = y1 - HNDL_SIZE - HNDL_OFFSET;
    
    gx_line->handles[3].x = x2 + HNDL_OFFSET;
    gx_line->handles[3].y = y1+(height/2) -HNDL_OFFSET;
    
    gx_line->handles[4].x = x2 + HNDL_OFFSET;
    gx_line->handles[4].y = y2 + HNDL_OFFSET;
    
    gx_line->handles[5].x = x1 + (width/2) - HNDL_OFFSET;
    gx_line->handles[5].y = y2 + HNDL_OFFSET;
    
    gx_line->handles[6].x = x1 - HNDL_OFFSET - HNDL_SIZE;
    gx_line->handles[6].y = y2 + HNDL_OFFSET;
    
    gx_line->handles[7].x = x1 - HNDL_OFFSET - HNDL_SIZE;
    gx_line->handles[7].y = y1+(height/2) -HNDL_OFFSET;
}

static void line_move( GXObjPtr line, XEvent *event ){
    static int x = 0, y = 0;
    GXLinePtr line_data = (GXLinePtr)line->data;
    int i;

    if( x && y ) {
        XDrawLines( XtDisplay(GxDrawArea),
        XtWindow(GxDrawArea), rubberGC,
        line_data->pts, line_data->num_pts,
        CoordModeOrigin );
    } else {
        /* our first time through */
        (*line->erase)( line );
        x = event ? event->xbutton.x : 0;
        y = event ? event->xbutton.y : 0;
    }

    if( event ) {
        for( i = 0; i < line_data->num_pts; i++ ) {
            line_data->pts[i].x += (event->xbutton.x - x);
            line_data->pts[i].y += (event->xbutton.y - y);
        }
        /*
        * draw rubberband line
        */
        XDrawLines( XtDisplay(GxDrawArea),
                    XtWindow(GxDrawArea), rubberGC,
                        line_data->pts, line_data->num_pts,
                        CoordModeOrigin );
        x = event->xbutton.x;
        y = event->xbutton.y;
    } else {
        x = 0;
        y = 0;
    }
}

static void line_scale( GXObjPtr line, XEvent *event ){

    static GXLinePtr tmp_data = NULL;

    GXLinePtr line_data = (GXLinePtr) line ->data;

    if( tmp_data ) {
        XDrawLines( XtDisplay (GxDrawArea),
                    XtWindow(GxDrawArea), rubberGC,
                    tmp_data->pts, tmp_data->num_pts,
                    CoordModeOrigin );

    } else {
        /* our first time... */
        (*line ->erase)( line );

        tmp_data = (GXLinePtr)XtNew(GXLine) ;
        tmp_data->num_pts = line_data->num_pts;
        tmp_data->pts = (XPoint *)
            XtMalloc( sizeof(XPoint) * tmp_data->num_pts );

        get_bounds( line_data->pts, line_data->num_pts,
                    &OrigX, &OrigY, &ExntX, &ExntY );
    }

    if( event ) {
        memcpy( (char *)tmp_data->pts, (char *)line_data->pts,
                sizeof(XPoint) * tmp_data->num_pts );

        apply_delta( tmp_data->pts, tmp_data->num_pts,
            FixedX - event->xbutton.x,
            FixedY - event->xbutton.y );

        XDrawLines( XtDisplay(GxDrawArea) ,
                    XtWindow(GxDrawArea), rubberGC,
                    tmp_data->pts, tmp_data->num_pts,
                    CoordModeOrigin );
    } else {
        if( tmp_data ) {
            memcpy( (char *)line_data->pts,
                    (char *)tmp_data->pts,
                    sizeof (XPoint) * line_data->num_pts );

            tFree((char *)tmp_data->pts);
            XtFree((char *)tmp_data);
            tmp_data = NULL;
        }
    }
}

static void line_copy( GXObjPtr line )
{
    int i;
    GXLinePtr temp_data;
    GXLinePtr line_data = (GXLinePtr)line->data;
    
    (*line->deselect)( line );

    temp_data = (GXLine *)XtNew(GXLine);
    temp_data->num_pts = line_data->num_pts;
    temp_data->pts = (XPoint *)
        XtMalloc(sizeof(XPoint) * temp_data->num_pts ); 

    for( i = 0; i < temp_data->num_pts; i++ ) {
        temp_data->pts[i].x = line_data->pts[i].x + OFFSET;
        temp_data->pts[i].y = line_data->pts[i].y + OFFSET;
    }
    create_line( NULL, temp_data );
    XtFree((char *)temp_data );
}    

static void line_save( FILE *fp, GXObjPtr obj ){
    int i;
    GXLinePtr line = (GXLinePtr)obj ->data;

    fprintf( fp, "LINE [numpts x y x y ...J\n" );

    fprintf( fp, "%sd\n", line->num_pts );

    for( i = 0; i < line->num_pts; i++ ) {
        fprintf( fp, "%d %d\n",
        line ->pts[i].x, line->pts[i].y );
    }
}


void gxLineLoad( FILE *fp, GxXObjPtr obj ){
    int i;
    GXLine line;
    fscanf( fp, "%d\n", &line.num_pts );
    line.pts = (XPoint *)

    XtMalloc(sizeof(XPoint) * line.num_pts);

    for( i = 0; i < line.num_pts; i++ ) {
        fscanf( fp, "%hd %hd\n",
            &line.pts[i].x, &line.pts[i].y );
    }
    create_line( obj, &line );
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
    printf( "draw freestlye\n" );
}
void gx_arrow( XEvent *event )
{
    printf( "draw an arrow\n" );
}
void gx_box( XEvent *event )
{
    printf( "draw a box\n" );
}

/**
** end of gxLine.c
*/