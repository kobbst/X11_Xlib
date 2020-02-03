/**
** 2D Graphical Editor (2d-gx)
**
** gxProtos.h
*/
#include <X11/Intrinsic.h>
#include "gxIcons.h"

#ifndef EXTERN
#define EXTERN
#else
#undef EXTERN
#define EXTERN extern
#endif

/*
* Creations routines in gxGraphics.c
*/
EXTERN Widget create_canvas ( Widget );
EXTERN void create_status ( Widget, Widget );
EXTERN void create_buttons( Widget );
EXTERN void drawAreaEventProc( Widget, XtPointer, XEvent *, Boolean );

/*
* Drawing functions used in GxIcons.h
*/
EXTERN void gx_line( void );
EXTERN void gx_pencil( void );
EXTERN void gx_arc( void );
EXTERN void gx_box( void );
EXTERN void gx_arrow( void );
EXTERN void gx_text( void );
/*
* Control functions used in GxIcons.h
*/
EXTERN void gx_exit( Widget, XtPointer, XtPointer );
/*
* Utilities in gxGx.c
*/
EXTERN void setStatus( char * );
EXTERN void draw_manager( Widget, XtPointer, XtPointer );
EXTERN void ctrl_manager( Widget, XtPointer, XtPointer );
/**
** end of gxProtos.h
*/