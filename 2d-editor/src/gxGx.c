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