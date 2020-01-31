/* Based on MIT's copywritten Template widget.  Modifications copyright circa
   1992 by James Morris and John Grotzinger.  Permissions to use etc are 
   granted as per those in the X11 copyright.  No warranties or guarantees 
   are made implicity or explicity; no responsiblity is taken for anything
   involving use of this software; no claims are made as to its performance 
   or any other aspect; use at your own risk. */

/* The Canvas widget is a very slightly modified Core.  It has a background
   pixmap that you draw into; it handles resizing it itself.  That's it.  You
   just use it as a drawing canvas, hence the clever name.  Class names etc
   are completely standard.  Functions to use are
   ClearCanvas(Widget canvas) --- clears the pixmap to the background color
   Pixmap CanvasPixmap(Widget canvas) --- the pixmap, so you can draw in it
   ExposeCanvas(Widget canvas) --- does a XClearArea on the pixmap, thus 
               sending appropriate Expose events so that the pixmap gets 
	       redrawn into the widget's window
   You usually use them in about that order --- Clear the canvas of old
   material; get the Pixmap; draw into it; Expose the new drawings.  */


#ifndef _Canvas_h
#define _Canvas_h

/****************************************************************
 *
 * Canvas widget
 *
 ****************************************************************/

/* Resources:  Core only, but default backgroundPixmap changed:

 Name		     Class		RepType		Default Value
 ----		     -----		-------		-------------
 backgroundPixmap    Pixmap	  	Pixmap	 	new, blank pixmap

Attempts to set backgroundPixmap will be ignored.		
'Blank' means it starts out filled with XtNbackground color.

*/

/* define any special resource names here that are not in <X11/StringDefs.h> */
  
  /* declare specific CanvasWidget class and instance datatypes */

typedef struct _CanvasClassRec*	        CanvasWidgetClass;
typedef struct _CanvasRec*		CanvasWidget;

/* declare the class constant */

extern WidgetClass canvasWidgetClass;
extern void ClearCanvas();
extern void ExposeCanvas();
extern Pixmap CanvasPixmap();

#endif /* _Canvas_h */