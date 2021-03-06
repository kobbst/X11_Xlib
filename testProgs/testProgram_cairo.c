/*
      ____      _____
    /\__  \   /\  ___\
    \/__/\ \  \ \ \__/_
        \ \ \  \ \____ \
        _\_\ \  \/__/_\ \
      /\ _____\  /\ _____\
      \/______/  \/______/

   Copyright (C) 2011 Joerg Seebohn

   This program is free software; you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation; either version 2 of the License, or
   (at your option) any later version.

   This program demonstrates how an X11 window with OpenGL support
   can be drawn transparent.

   The title bar and window border drawn by the window manager are
   drawn opaque.
   Only the background of the window which is drawn with OpenGL
         glClearColor( 0.7, 0.7, 0.7, 0.7) ;
         glClear(GL_COLOR_BUFFER_BIT) ;
   is 30% transparent.

   Compile it with: 
   	 gcc -std=gnu99 -o test testprogram.c -lX11 -lGL
*/
#include <X11/Xlib.h>
#include <X11/Xatom.h>
#include <X11/keysym.h>

#include <GL/gl.h>
#include <GL/glx.h>

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include <cairo.h>
#include <cairo-xlib.h>

void draw(cairo_surface_t *sfc){

   cairo_t *ctx;
   ctx = cairo_create(sfc);
   cairo_set_source_rgb(ctx, 1, 1, 1);
   cairo_paint(ctx);
   cairo_move_to(ctx, 20, 20);
   cairo_line_to(ctx, 200, 400);
   cairo_line_to(ctx, 450, 100);
   cairo_line_to(ctx, 20, 20);
   cairo_set_source_rgb(ctx, 0, 0, 1);
   cairo_fill_preserve(ctx);
   cairo_set_line_width(ctx, 5);
   cairo_set_source_rgb(ctx, 1, 1, 0);
   cairo_stroke(ctx);
   cairo_destroy(ctx);
}

cairo_surface_t *cairo_create_x11_surface0(Display *dsp, Window da, int x, int y)
{
   
   // Drawable da;
   
   int screen;
   cairo_surface_t *sfc;

   screen = DefaultScreen(dsp);
   // da = XCreateSimpleWindow(dsp, DefaultRootWindow(dsp), 0, 0, x, y, 0, 0, 0);
   XSelectInput(dsp, da, ButtonPressMask | KeyPressMask);
   XMapWindow(dsp, da);

   sfc = cairo_xlib_surface_create(dsp, da, DefaultVisual(dsp, screen), x, y);
   cairo_xlib_surface_set_size(sfc, x, y);

   return sfc;
}


void cairo_close_x11_surface(cairo_surface_t *sfc)
{
   Display *dsp = cairo_xlib_surface_get_display(sfc);

   cairo_surface_destroy(sfc);
   XCloseDisplay(dsp);
}

int cairo_check_event(cairo_surface_t *sfc, int block)
{
   char keybuf[8];
   KeySym key;
   XEvent e;

   for (;;)
   {
      // draw(sfc);
  /*       if (block || XPending(cairo_xlib_surface_get_display(sfc)))
         XNextEvent(cairo_xlib_surface_get_display(sfc), &e);
      else 
         return 0;
 */
      switch (e.type)
      {
         case ButtonPress:
            return -e.xbutton.button;
         case KeyPress:
            XLookupString(&e.xkey, keybuf, sizeof(keybuf), &key, NULL);
            return key;
         // default:
            // fprintf(stderr, "Dropping unhandled XEevent.type = %d.\n", e.type);
      }
      
   }
}


Window getWindow(Display * display,unsigned int x,unsigned int y,
                           unsigned int width,unsigned int height)
{  
   XVisualInfo visualinfo ;
   XMatchVisualInfo(display, DefaultScreen(display), 32, TrueColor, &visualinfo);
   
   XSetWindowAttributes attr ;
   attr.colormap   = XCreateColormap( display, DefaultRootWindow(display), visualinfo.visual, AllocNone) ;
   attr.event_mask = ExposureMask | KeyPressMask ;
   attr.background_pixmap = None ;
   attr.border_pixel      = 0 ;
   return XCreateWindow( display, DefaultRootWindow(display),
                           x,y,width,height, //: are possibly opverwriteen by window manager
                           0,
                           visualinfo.depth,
                           InputOutput,
                           visualinfo.visual,
                           /* CWColormap|CWEventMask|CWBackPixmap|CWBorderPixel, */
                           CWColormap | CWBorderPixel | CWBackPixel,
                           &attr
                        ) ;

}


int main(int argc, char* argv[])
{
   Display    * display = XOpenDisplay( 0 ) ;
   const char * xserver = getenv( "DISPLAY" ) ;

    int  x, y, width, height, i; 
    int  w_width = 420, w_height= 520; 

   if (display == 0)
   {
      printf("Could not establish a connection to X-server '%s'\n", xserver ) ;
      exit(1) ;
   }

   // query Visual for "TrueColor" and 32 bits depth (RGBA)
   XVisualInfo visualinfo ;
   XMatchVisualInfo(display, DefaultScreen(display), 32, TrueColor, &visualinfo);
   
   // create window
   Window   win ;
   GC       gc ;
   XSetWindowAttributes attr ;
   attr.colormap   = XCreateColormap( display, DefaultRootWindow(display), visualinfo.visual, AllocNone) ;
   attr.event_mask = ExposureMask | KeyPressMask ;
   attr.background_pixmap = None ;
   attr.border_pixel      = 0 ;
   win = XCreateWindow(    display, DefaultRootWindow(display),
                           550, 200, 400, 600, // x,y,width,height : are possibly opverwriteen by window manager
                           0,
                           visualinfo.depth,
                           InputOutput,
                           visualinfo.visual,
                           /* CWColormap|CWEventMask|CWBackPixmap|CWBorderPixel, */
                           CWColormap | CWBorderPixel | CWBackPixel,
                           &attr
                        ) ;
   gc = XCreateGC( display, win, 0, 0) ;

   cairo_surface_t *sfc;
   // sfc = cairo_create_x11_surface0(display, win, 600, 600);

   // set title bar name of window
   XStoreName( display, win, "Transparent Window with OpenGL Support" ) ;

   // say window manager which position we would prefer
   XSizeHints sizehints ;
   sizehints.flags = PPosition | PSize ;
   sizehints.x     = 250 ;  sizehints.y = 300 ;
   sizehints.width = 400 ; sizehints.height = 600 ;
   XSetWMNormalHints( display, win, &sizehints ) ;
   // Switch On >> If user pressed close key let window manager only send notification >>
   Atom wm_delete_window = XInternAtom( display, "WM_DELETE_WINDOW", 0) ;
   XSetWMProtocols( display, win, &wm_delete_window, 1) ;

   { 
      // change foreground color to brown
      XColor    xcol ;
      xcol.red   = 153 * 256 ;   // X11 uses 16 bit colors !
      xcol.green = 116 * 256 ;
      xcol.blue  = 65  * 256 ;
      XAllocColor( display, attr.colormap, &xcol) ;
      XGCValues gcvalues ;
      gcvalues.foreground = xcol.pixel ;
      XChangeGC( display, gc, GCForeground, &gcvalues) ;
   }

   // create OpenGL context
   GLXContext glcontext = glXCreateContext( display, &visualinfo, 0, True ) ;
   if (!glcontext)
   {
      printf("X11 server '%s' does not support OpenGL\n", xserver ) ;
      exit(1) ;
   }
   glXMakeCurrent( display, win, glcontext ) ;

   // now let the window appear to the user
   XMapWindow( display, win) ;

   int isUserWantsWindowToClose = 0 ;

   while( !isUserWantsWindowToClose )
   {
      int  isRedraw = 0 ;

      /* XPending returns number of already queued events.
       * If no events are queued XPending sends all queued requests to the X-server
       * and tries to read new incoming events. */

      while( XPending(display) > 0 )
      {
         // process event
         XEvent   event ;
         XNextEvent( display, &event) ;

         switch(event.type)
         {  // see 'man XAnyEvent' for a list of available events
         case ClientMessage:
                  // check if the client message was send by window manager to indicate user wants to close the window
                  if (  event.xclient.message_type  == XInternAtom( display, "WM_PROTOCOLS", 1)
                        && event.xclient.data.l[0]  == XInternAtom( display, "WM_DELETE_WINDOW", 1)
                        )
                  {
                     isUserWantsWindowToClose = 1 ;
                  }
         case KeyPress:
                  if (XLookupKeysym(&event.xkey, 0) == XK_Escape)
                  {
                     isUserWantsWindowToClose = 1 ;
                  }
                  break ;
         case Expose:
                  isRedraw = 1 ;
                  break ;
         default:
               // do no thing
               break ;
         }

      }

      // ... all events processed, now do other stuff ...

      if (isRedraw)
      {  // needs redraw
         // use opengl to clear background in (transparent) light grey
         glClearColor( 0.7, 0.7, 0.7, 0.7) ;
         glClear(GL_COLOR_BUFFER_BIT) ;cairo_create_x11_surface0
         glXSwapBuffers( display, win) ;
         glXWaitGL() ;
         // draw string with X11
         XDrawString( display, win, gc, 10, 20, "Hello ! ", 7) ;
      }

      // ... do something else ...

      // int vl = &DefaultDepth(display,win);
      // char buffer[20];

      // itoa(vl, &buffer,10);
      // sprintf(buffer, "%d", buffer);

      XDrawString( display, win, gc, 10, 20, DisplayString(display), 7) ;

         width=30;  height=30;  i=0; 
         for( y=2 ; y<w_height ; y+=35 ) 
         { 
            for( x=2 ; x<w_width ; x+=35 ) 
            { 
                  XSetForeground( display, gc, 255 + (100<<8) + (100<<16) );     
                  // XFillRectangle( display, win, gc, x, y + 40, width, height ); 
                  XDrawRectangle( display, win, gc, x, y + 40, width, height ); 
                  i++; 
            } 
         } 
         draw(sfc);
      
   }


   cairo_check_event(sfc, 1);
   cairo_close_x11_surface(sfc);
   XDestroyWindow( display, win ) ; 
   win = 0 ;
   XCloseDisplay( display ) ; 
   display = 0 ;

   return 0 ;
}
