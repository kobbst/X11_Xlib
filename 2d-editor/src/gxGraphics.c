/**
** 2D Graphical Editor (2d -gx)
** gxGraphics.c
*/
#include <stdio.h>
#include <X11/Intrinsic.h>
#include <X11/StringDefs.h>
#include <X11/Xaw/Form.h>
#include <X11/Xaw/Command.h>
#include <X11/Xaw/Box.h>
#include <X11/cursorfont.h>
#include "../include/gxGraphics.h"
#include "../include/gxIcons.h"
#include "../include/gxBitmaps.h"

static GxIconData gxDrawIcons[] = {
    { &line_icon, (void (*)(void))gx_line,
    "Draw an elastic line..."
    },
    { &pen_icon, (void (*)(void))gx_pencil,
    "Draw a freestyle line..."
    },
    { &arc_icon, (void (*)(void))gx_arc,
    "Draw a circle..."
    },
    { &box_icon, (void (*)(void))gx_box,
    "Draw a square or rectangle..." },
    { &arr_icon, (void (*)(void))gx_arrow,
    "Draw an arrow..."
    },
    { &text_icon, (void (*)(void))gx_text,
    "Draw dynamic text..."
    },
    /*-----------------------------------*/
    /* this list MUST be NULL terminated */
    /*-----------------------------------*/
    { NULL },
};

/*
* Create the region of the application where we will draw
*/
Widget create_canvas( Widget parent )
{
    GxDrawArea = XtVaCreateWidget( "drawingArea",
                                    formWidgetClass, parent,
                                    XtNbackground,
                                    WhitePixelOfScreen(XtScreen(parent)),
                                    XtNtop, XawChainTop,
                                    XtNleft, XawChainLeft,
                                    XtNbottom , XawChainBottom,
                                    XtNright, XawChainRight,
                                    XtNheight, 220,
                                    XtNwidth, 250,
                                    NULL );
    XtAddEventHandler( GxDrawArea, PointerMotionMask, False,
                    (XtEventHandler)drawAreaEventProc,
                    (XtPointer)NULL);
    XtAddEventHandler( GxDrawArea, ButtonPressMask, False,
                    (XtEventHandler)drawAreaEventProc,
                    (XtPointer)NULL);
    XtAddEventHandler( GxDrawArea, ButtonReleaseMask, False,
                    (XtEventHandler)drawAreaEventProc,
                    (XtPointer)NULL);


    return GxDrawArea;
}                                

/*
* create_status
*/
void create_status( Widget parent, Widget fvert )
{
    GxStatusBar = XtVaCreateManagedWidget( "statusBar",
                labelWidgetClass, parent,
                XtNtop,     XawChainBottom,
                XtNleft,    XawChainLeft,
                XtNbottom,  XawChainBottom,
                XtNright,   XawChainRight,
                XtNfromVert, fvert,
                XtNborderWidth, 0,
                NULL );


    setStatus( "2D-GX (c)Starry Knight Software - Ready..." );
}

/*
* statusProc
*/
void statusProc( Widget w, XtPointer msg, XEvent *xe, Boolean flag )
{
    if( msg == NULL )
        setStatus( "\0" );
    else
        setStatus( msg );
}

/*
* create icons
*/
void create_icons( Widget parent, GxIconData *iconData,
                  void (*callback) ( Widget, XtPointer, XtPointer ))
{
    Widget btn;
    Pixmap pix;

    while( iconData->info != NULL ) {
            if( iconData->info->bits != NULL ) {
                pix = create_pixmap( parent, iconData->info );

               btn =   XtVaCreateManagedWidget( "", commandWidgetClass, parent,
                XtNwidth, iconData->info->w + 1,
                XtNheight, iconData->info->h + 1,
                XtNbackgroundPixmap, pix,
                XtNhighlightThickness, 1,
                NULL );

                XtAddEventHandler( btn, EnterWindowMask, False, (XtEventHandler) statusProc,
                            (XtPointer)iconData->mesg);
                XtAddEventHandler( btn, LeaveWindowMask, False, (XtEventHandler) statusProc,
                            (XtPointer)NULL );

                XtAddCallback( btn, XtNcallback, callback, (XtPointer)iconData->func );
        }
        
        /*
        * go to the next element
        */
        iconData++;
    }
}
/*

* Create a panel of buttons that will
* allow control of the application
*/
void create_buttons( Widget parent )
{
    Widget butnPanel, exitB;

    /*
    * create a panel for the drawing icons
    */

    butnPanel = XtVaCreateWidget( "drawButnPanel", 
                                boxWidgetClass, parent,
                                XtNtop, XawChainTop,
                                XtNright, XawChainRight ,
                                XtNbottom, XawChainTop,
                                XtNleft, XawChainRight ,
                                XtNhorizDistance, 10,
                                XtNfromHoriz, GxDrawArea,
                                XtNhSpace, 1,
                                XtNvSpace, 1,
                                NULL );

    create_icons( butnPanel, gxDrawIcons, draw_manager );
    XtManageChild( butnPanel );

    exitB = XtVaCreateManagedWidget( "  Exit ",
                                    commandWidgetClass, parent,
                                    XtNtop, XawChainBottom,
                                    XtNbottom, XawChainBottom,
                                    XtNleft, XawChainRight,
                                    XtNright, XawChainRight,
                                    XtNfromVert, butnPanel,
                                    XtNfromHoriz, GxStatusBar,
                                    NULL );

    XtAddCallback( exitB, XtNcallback, gx_exit, NULL );
}
/*
* create_pixmap
*/
Pixmap create_pixmap( Widget w, XbmDataPtr data )
{
    return( XCreatePixmapFromBitmapData( XtDisplay(w),
                        DefaultRootWindow(XtDisplay(w) ),
                        (char *)data->bits,
                        data->w, data->h,
                        BlackPixelOfScreen(XtScreen(w)),
                        WhitePixelOfScreen(XtScreen(w)),
                        DefaultDepthOfScreen(XtScreen(w))));

}
/**
** end of gxGraphics.c
*/