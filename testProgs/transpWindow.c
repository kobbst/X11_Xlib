#include <X11/Xlib.h>
#include <X11/Xutil.h>

int main(int argc, char* argv[])
{
    Display* display = XOpenDisplay(NULL);

    int  x, y, width, height, i; 
    int  w_width = 420, w_height= 520; 

    XVisualInfo vinfo;
    XMatchVisualInfo(display, DefaultScreen(display), 32, TrueColor, &vinfo);

    XSetWindowAttributes attr;
    attr.colormap = XCreateColormap(display, DefaultRootWindow(display), vinfo.visual, AllocNone);
    attr.border_pixel = 0;
    attr.background_pixel = 0;

    Window win2 = XCreateWindow(display, DefaultRootWindow(display), 
                            0, 0, w_width, w_height, 0, 
                            vinfo.depth, InputOutput, vinfo.visual,
                            CWColormap | CWBorderPixel | CWBackPixel, &attr); 
/* 
    Window win = XCreateWindow(display, DefaultRootWindow(display), 0, 0, 300, 200, 1, vinfo.depth, 
                                InputOutput, vinfo.visual, CWColormap | CWBorderPixel | CWBackPixel, &attr);*/

    Window win = XCreateSimpleWindow( display, DefaultRootWindow(display), 
                            0, 0, w_width, w_height, 1, 
                            BlackPixel(display,0), WhitePixel(display,0) );                                

    Atom wm_delete_window = XInternAtom(display, "WM_DELETE_WINDOW", 0);
    XSetWMProtocols(display, win, &wm_delete_window, 1);

    XMapWindow(display, win);
    
    GC gc = XCreateGC(display, win, 0, 0);


    width=16;  height=16;  i=0; 
    for( y=2 ; y<w_height ; y+=20 ) 
    { 
        for( x=2 ; x<w_width ; x+=20 ) 
        { 
            XSetForeground( display, gc, i );     
            XFillRectangle( display, win, gc, x, y, width, height ); 
            i++; 
        } 
    } 
    XSelectInput(display, win, StructureNotifyMask);
    XFlush(display);



    // XStoreName( display, win, "Color Map" ); 


    int keep_running = 1;
    XEvent event;

    while (keep_running) {
        XNextEvent(display, &event);

        switch(event.type) {
            case ClientMessage:
                if (event.xclient.message_type == XInternAtom(display, "WM_PROTOCOLS", 1) && (Atom)event.xclient.data.l[0] == XInternAtom(display, "WM_DELETE_WINDOW", 1))
                    keep_running = 0;

                break;

            default:
                break;
        }
    }

    XDestroyWindow(display, win);
    XCloseDisplay(display);
    return 0;
}