/* Based on MIT's copywritten Template widget.  Modifications copyright circa
   1992 by James Morris and John Grotzinger.  Permissions to use etc are 
   granted as per those in the X11 copyright.  No warranties or guaranties 
   are made implicity or explicity; no responsiblity is taken for anything 
   involving use of this software; no claims are made as to its performance 
   or any other aspect; use at your own risk. */

#ifndef _CanvasP_h
#define _CanvasP_h

#include "Canvas.h"
/* include superclass private header file */
#include <X11/CoreP.h>

/* define unique representation types not found in <X11/StringDefs.h> */

typedef struct {
    int empty;
} CanvasClassPart;

typedef struct _CanvasClassRec {
    CoreClassPart	core_class;
    CanvasClassPart	canvas_class;
} CanvasClassRec;

extern CanvasClassRec canvasClassRec;

typedef struct {
    /* resources */
    /* none */

    /* private state */
    GC gc;
    Pixmap pixmap;  /* same as backgroundPixmap */
    Boolean needpixmap;
} CanvasPart;

typedef struct _CanvasRec {
    CorePart   	core;
    CanvasPart	canvas;
} CanvasRec;

#endif /* _CanvasP_h */