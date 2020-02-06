#include <X11/Xlib.h>
#include <X11/Xutil.h>
#include <stdio.h>
#include <unistd.h>
 
#define WIN_WIDTH   640
#define WIN_HEIGHT  480
 
unsigned long GetColor( Display* dis, char* color_name )
{
    Colormap cmap;
    XColor near_color, true_color;
 
    cmap = DefaultColormap( dis, 0 );
    XAllocNamedColor( dis, cmap, color_name, &near_color, &true_color );
    return( near_color.pixel );
}
 
int main( void )
{
    Display* dis;
    Window win;
    XSetWindowAttributes att;
    GC gc;
    XEvent ev;
 
    int t;
 
    dis = XOpenDisplay( NULL );
    win = XCreateSimpleWindow( dis, RootWindow(dis,0), 100, 100, WIN_WIDTH, WIN_HEIGHT, 5, WhitePixel(dis,0), BlackPixel(dis,0) );
 
    att.backing_store = WhenMapped;
    XChangeWindowAttributes( dis, win, CWBackingStore, &att );
 
    XSelectInput( dis, win, ExposureMask );
    XMapWindow( dis, win );
 
    do{
        XNextEvent( dis, &ev);
    }while( ev.type != Expose );
 
    gc = XCreateGC( dis, DefaultRootWindow(dis), 0, 0 );
    XSetFunction( dis, gc, GXxor );
    XSetForeground( dis, gc, BlackPixel(dis,0)^GetColor( dis, "blue")  );
 

    XFillRectangle( dis, win, gc, 0, 0, 50, 50 );
    XDrawLine( dis, win, gc, 50, 50, 100, 100);
    XDrawRectangle( dis, win, gc, 10, 280, 80, 40);
    XDrawPoint( dis, win, gc, 500, 50);


    XSetLineAttributes ( dis, gc, 10,0,0,0);
    XDrawArc( dis, win, gc, 150, 150, 250, 250, 0, 360*64);

    XFillArc( dis, win, gc, 300, 300, 20, 20, 0, 360*64);
 
    XFlush( dis ); 

    for (int t = 0; t < 300; t++)
        usleep(100000);

    XDestroyWindow( dis , win );
    XCloseDisplay( dis );

    return(0);

}