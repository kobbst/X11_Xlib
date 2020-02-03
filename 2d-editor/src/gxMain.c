/**
** 2D Graphical Editor (2d-qx)
**
** gxMain.c
*/
#include <stdlib.h>
#include <stdio.h>
#include <X11/Intrinsic.h>  /* for creation routines */
#include <X11/StringDefs.h> /* resource names */
#include <X11/Xaw/Form.h>
#include "gxGraphics.h"
#include "gxProtos.h"

/*
* Entry point for the application
*/

int main( int argc, char **argv )
{
    XtAppContext appContext;
    Widget toplevel;
    Widget form;
    Widget canvas;
    toplevel = XtVaAppInitialize( &appContext, "2D Graphical Editor",
                                    NULL, 0, &argc, argv, NULL,
                                    NULL );
    form = XtVaCreateWidget( "topForm",
                            formWidgetClass, toplevel,
                            NULL );
    canvas = create_canvas( form );

    create_status( form, canvas );

    create_buttons( form );

    XtManageChild( canvas );
    XtManageChild( form );
    XtRealizeWidget( toplevel );
    XtAppMainLoop( appContext );
    exit(0); /* never reached as XtAppMainLoop is infinite */
}
/**
** end of gxMain.c
*/

