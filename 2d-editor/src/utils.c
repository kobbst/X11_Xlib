

#include <X11/Xaw/Dialog.h>
#include <X11/Xaw/StringDefs.h>

static char *gxGetFileName( void )
{
    XtAppContext app;

    XEvent event;
    Widget dialog;

    char *str = NULL;

    dialog = XtVaCreateManagedWidget( "Filename...", dialogWidgetClass,
                                    GxDrawArea,
                                    XtNwidth, 115,
                                    XtNheight, 70,
                                    XtNlabel, "Enter File:",
                                    XtNvalue, "",
                                    NULL );

    XawDialogAddButton( dialog, " Ok ", close_dialog, dialog );

    app = XtWidgetToApplicationContext( GxDrawArea );
    while( XtIsManaged(dialog)) {
        XtAppNextEvent( app, &event );
        XtDispatchEvent( &event );

    }
    str = XawDialogGetValueString( dialog );
    XtDestroyWidget( dialog );
    
    /*
    * look for 'illegal' characters
    */
        {
        int c, indx = 0;
        char illegal_chars[] = { '\n', 'z' };
        while( (c = (int)str[indx]) != '\0' ) {
            if( strchr( illegal_chars, c ) != NULL ) {
            str[indx] = '\0';
            break;
            }
            indx++;
        }
        /*
        * remove leading zeros
        */
            while( *str && *str == '' ) str++;
        }
        if( str && *str )
            return XtNewString(str);
        else
            return NULL;
}

static void close_dialog( Widget w, XtPointer cdata, XtPointer cbs )
{
    Widget dialog = (Widget)cdata;
    if( dialog ) XtUnmanageChild( dialog );
}