/**
** 2D Graphical Editor (2d-gx)
**
** gxText.c
*/
#include <stdio.h>
#include "gxGraphics.h"

static void GXDrawText( GXTextPtr text, GC gc ){
    char *txt = text->text;
    int c, chr, nsegs, num_pts;
        for( c = 0; c < text->len; c++, txt++ ) {
            chr = *txt - ' ';
            nsegs = 0;

            while( text->font[chr][nsegs] != NULL ) {
                num_pts = text->fontp[chr] [nsegs];
                if( num_pts > 0) {
                    XDrawLines( XtDisplay(GxDrawArea),
                                XtWindow(GxDrawArea), gc,
                                text ->vpts[c][nsegs], num_pts,
                                CoordModeOrigin );
                }
                nsegs++;
            }
        }
}

static GXTextPtr update_gxtext( XEvent *xe, char *str, GXTextPtr upd ){
    GXTextPtr text = NULL;
    if( upd ) {
        reset_pts( upd, xe->xbutton.x, xe->xbutton.y, 0, 0 );
    } else {
    text = create_gxtext( str, xe->xbutton.x,
                            xe->xbutton.y,
                            plain_simplex,
                            plain_simplex_p );
    }
    return text;
}

static void close_dialog(Widget w, XtPointer cdata, XtPointer cbs)
{
    Widget dialog = (Widget)cdata;
    if( dialog ) XtUnmanageChild( dialog );
}

static char *get_creation_text( void ){
    XtAppContext app;
    XEvent event;
    Widget dialog;
    char *str = NULL;
    dialog = XtVaCreateManagedWidget( "Text Entry Box",
                                        dialogWidgetClass,
                                        GxDrawArea,
                                        XtNwidth, 115,
                                        XtNheight, 70,
                                        XtNlabel,
                                        "Enter Text:",
                                        XtNvalue, "",
                                        NULL );
    XawDialogAddButton( dialog, " Ok ",  close_dialog, dialog );

    app = XtWidgetToApplicationContext( GxDrawArea );
    
    while( XtIsManaged(dialog)) {
        XtAppNextEvent( app, &event );
        XtDispatchEvent( &event );
    }
    str = XawDialogGetValueString( dialog );
    XtDestroyWidget( dialog );
    /*
    * look for ‘illegal’ characters
    */
    {
        int c, indx = 0;
        char illegal_chars[] = { '\n', '\r' };
        while( (c = (int)str[indx]) != '\0' ) {
            if( strchr( illegal_chars, c ) != NULL )
                str[indx] = ' ';
                indx++;
        }
        /*
        * remove leading zeros
        */
        while( *str && *str == ' ' ) str++;
    }
    if( str && *str )
        return XtNewString(str);
    else
        return NULL;
}

static void place_creation_text( XEvent *event, char ** text ){
    static GXTextPtr rubber_text = NULL;
    char *text = NULL;
    if( event == NULL ) {
        rubber_text = NULL;
    } else {
        if( _text ) text = *_text;

        if( rubber_text ) {
            GXDrawText( rubber_text, rubberGC );
        }
        switch( event->type ) {
            case ButtonPress:
                if( rubber_text && text ){
                    create_text( NULL, rubber_text );
                    gx_refresh();
                    freeGXText( rubber_text );
                    rubber_text = NULL;

                    free( text );
                    * text = NULL;

                    set_cursor( NORMAL_MODE );
                }
                break;

            case MotionNotify:

                if( text ) {
                    if( rubber text == NULL ) {
                            rubber text = update gxtext( event, text, NULL );
                        } else {
                        (void)update_gxtext( event, text, rubber_text );

                        }
                        GXDrawText( rubber_text, rubberGC );
                    }
                    break;
        }
    }
}


void gx_text( XEvent *event ){
    static char *creation_text = NULL;
    if( event == NULL ) {
        creation_text = NULL;
        place_creation_text( NULL, NULL );

        /*
        * we have to prompt for a string
        */
        creation_text = get_creation_text();

        if( creation_text != NULL )
                set_cursor( TEXT_MODE );

     } else {
        /*
        * If we have a string, place it!
        */
        if( creation_text ) {
                /* adjust for the hotspot in our cursor */
                event ->xbutton.y -= 10;
                event->xbutton.x += 10;

                place_creation_text( event, &creation_text );
        }
    }
}
/**
void gx_text( void )
{
    printf( “draw text...\n” );
}

** end of gxText.c
*/
