/*
** 20 Graphical Editor (2d-gx)
**
** gx6x.c
*/
#include <stdio.h>

#include "gxGraphics.h"
#include "gxProtos.h"

#include <X11/Xaw/Label.h>



static void (*draw_mgr_func)( XEvent * ) = NULL;
/*
* gx exit
*/
void gx_exit( Widget w, XtPointer cd, XtPointer cbs )
{
    exit(0);
}
/*
* setStatus
*/
void setStatus( char *message )
{
    XtVaSetValues( GxStatusBar, XtNlabel, message, NULL );
}
/*
* draw manager
*/
void draw_manager ( Widget w, XtPointer cd, XtPointer cbs )
{
    void (*draw_func)( XEvent * ) = (void (*)(XEvent *))cd;
    if( draw_func != NULL ) (*draw_func)( NULL );
    draw_mgr_func = draw_func;
}
/*
* drawAreaEventProc
*/
void drawAreaEventProc( Widget w, XtPointer cd, XEvent *event, Boolean flag )
{
    if( draw_mgr_func != NULL ) (*draw_mgr_func) ( event );
}

/**
** end of gxGx.c
*/

static void null_func( void ){
    printf( "Warning: null function called!\n" );
}

GXObjPtr gx_create_obj( void ){
    GXObjPtr gx_obj = XtNew( GXObj );

    gx_obj->fs = None;
    gx_obj->ls = LineSolid;
    gx_obj->lw = 1;
    gx_obj->bg = WhitePixelOfScreen(XtScreen(GxDrawArea));
    gx_obj->fg = BlackPixelOfScreen(XtScreen(GxDrawArea));
    gx_obj->handles = NULL;

    gx_obj->num_handles = 0;
    gx_obj->data = NULL;
    gx_obj->draw =      (void (*) ())   null_func;
    gx_obj->erase =     (void (*) ())   null_func;
    gx_obj->find =      (Boolean (*)()) null_func;
    gx_obj->select =    (void (*) ())   null_func;
    gx_obj->deselect =  (void (*) ())   null_func;
    gx_obj->move =      (void (*) ())   null_func;
    gx_obj->scale =     (void (*) ())   null_func;
    gx_obj->copy =      (void (*) ())   null_func;
    gx_obj->action = NULL;
    gx_obj->next = NULL;
    /* reset the draw mgr func so  */
    /* future events are applied to */
    /* existing objects */
    draw_mgr_func = NULL;
    return gx_obj;
}

void gx_add_obj( GXObjPtr obj ){
    GXObjPtr gx_obj;
    if( gxObjHeader == NULL ) {
        gxObjHeader = obj;
    } else {
        gx_obj = gxObjHeader;
        /* find the end of the object list */
        while( gx_obj->next != NULL ) {
            gx_obj = gx_obj->next;
        }
        /*
        * add the new object to the end of our list
        */
        gx_obj->next = obj;
    }
}

GC gx_allocate_gc( GXObjPtr obj, Boolean tile )
{
    GC gc;
    XGCValues values;
    XtGCMask mask = 0L;
    values.foreground = obj->fg;
    mask |= GCForeground;

    values.background = obj->bg;
    mask |= GCBackground;

    values.line_width = obj->lw;
    mask |= GCLineWidth;

    values.line_style = obj->ls;
    mask |= GCLineStyle;

    if( tile ) {
        values.tile = GxDrawAreaBG;
        mask |= GCTile;
        values.fill_style = FillTiled;
        mask |= GCFillStyle;
    }
    values.function = GXcopy;
    mask |= GCFunction;
    
    gc = XtAllocateGC(GxDrawArea, 0, mask, &values, mask, 0);
    return gc;
}
