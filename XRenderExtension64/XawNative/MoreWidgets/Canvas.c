/* Based on MIT's copywritten Template widget.  modifications copyright circa
   1992 by James Morris and John Grotzinger.  Permissions to use etc are 
   granted as per those in the X11 copyright.  No warranties or guarantees 
   are made implicity or explicity; no responsiblity is taken for anything 
   involving use of this software; no claims are made as to its performance 
   or any other aspect; use at your own risk.  */

#include <X11/IntrinsicP.h>  
#include <X11/StringDefs.h>  
#include "CanvasP.h"

static void Initialize(Widget request, Widget new, 
		       ArgList arglist, Cardinal *num_args)
{
  ((CanvasWidget)new)->canvas.needpixmap = True;
  return;
}

static void Newpixmap(Widget w, XEvent *junkevent, Region junkregion)
{
  CanvasWidget cw = (CanvasWidget)w;
  Arg args[4];
  Dimension width, height;
  Cardinal depth;
  Pixel fg;
  Display *disp = XtDisplay(w);
  Window win = XtWindow(w);

  if (!(cw->canvas.needpixmap)) return;

  cw->canvas.gc = XCreateGC(disp, win, 0, 0);
  XtSetArg(args[0], XtNwidth, &width);
  XtSetArg(args[1], XtNheight, &height);
  XtSetArg(args[2], XtNdepth, &depth);
  XtSetArg(args[3], XtNbackground, &fg);
  XtGetValues(w, args, 4);
  cw->canvas.pixmap = XCreatePixmap(disp, win, width, height, depth);
  XSetForeground(disp, cw->canvas.gc, fg);
  XFillRectangle(disp, cw->canvas.pixmap, cw->canvas.gc, 0, 0, width, height);
  cw->canvas.needpixmap = False;
  XtSetArg(args[0], XtNbackgroundPixmap, cw->canvas.pixmap);
  XtSetValues(w, args, 1);
}

static void Repixmap(Widget w)
{
  Arg args[4];
  Dimension width, height;
  Cardinal depth;
  Pixmap pmap;
  Pixel fg;
  CanvasWidget cw = (CanvasWidget)w;
  Display *disp = XtDisplay(w);

  XtSetArg(args[0], XtNwidth, &width);
  XtSetArg(args[1], XtNheight, &height);
  XtSetArg(args[2], XtNdepth, &depth);
  XtSetArg(args[3], XtNbackground, &fg);
  XtGetValues(w, args, 4);

  pmap = XCreatePixmap(disp, XtWindow(w), width, height, depth);
  XSetForeground(disp, cw->canvas.gc, fg);
  XFillRectangle(disp, pmap, cw->canvas.gc, 0, 0, width, height);
  XCopyArea(disp, cw->canvas.pixmap, pmap, cw->canvas.gc, 0, 0, width, height, 
	    0, 0);
  XFreePixmap(disp, cw->canvas.pixmap);
  cw->canvas.pixmap = pmap;
  XtSetArg(args[0], XtNbackgroundPixmap, pmap);
  XtSetValues(w, args, 1);
}

static Boolean CanvasSetValues(Widget old, Widget request, Widget new, 
			       ArgList args, Cardinal *numargs)
{
  /* if backgroundPixmap was changed --- change it back! */

  if (!((CanvasWidget)new)->canvas.needpixmap)
    ((CoreWidget)new)->core.background_pixmap =
      ((CanvasWidget)new)->canvas.pixmap;

  return False;
}

static void DestroyCanvas(Widget w)
{
  CanvasWidget cw = (CanvasWidget)w;
  Display *disp = XtDisplay(w);

  XFreeGC(disp, cw->canvas.gc);
  XFreePixmap(disp, cw->canvas.pixmap);
}

CanvasClassRec canvasClassRec = {
  { /* core fields */
    /* superclass		*/	(WidgetClass) &widgetClassRec,
    /* class_name		*/	"Canvas",
    /* widget_size		*/	sizeof(CanvasRec),
    /* class_initialize		*/	NULL,
    /* class_part_initialize	*/	NULL,
    /* class_inited		*/	FALSE,
    /* initialize		*/	Initialize,
    /* initialize_hook		*/	NULL,
    /* realize			*/	XtInheritRealize,
    /* actions			*/	NULL,
    /* num_actions		*/	0,
    /* resources		*/	NULL,
    /* num_resources		*/	0,
    /* xrm_class		*/	NULLQUARK,
    /* compress_motion		*/	TRUE,
    /* compress_exposure	*/	TRUE,
    /* compress_enterleave	*/	TRUE,
    /* visible_interest		*/	FALSE,
    /* destroy			*/	DestroyCanvas,
    /* resize			*/	Repixmap,
    /* expose			*/	Newpixmap,
    /* set_values		*/	CanvasSetValues,
    /* set_values_hook		*/	NULL,
    /* set_values_almost	*/	XtInheritSetValuesAlmost,
    /* get_values_hook		*/	NULL,
    /* accept_focus		*/	NULL,
    /* version			*/	XtVersionDontCheck,
    /* callback_private		*/	NULL,
    /* tm_table			*/	NULL,
    /* query_geometry		*/	XtInheritQueryGeometry,
    /* display_accelerator	*/	XtInheritDisplayAccelerator,
    /* extension		*/	NULL
  },
  { /* canvas fields */
    /* empty			*/	0
  }
};

WidgetClass canvasWidgetClass = (WidgetClass)&canvasClassRec;

void ClearCanvas(Widget w)
{
  CanvasWidget cw = (CanvasWidget)w;
  Arg args[4];
  Dimension width, height;
  Cardinal depth;
  Pixel fg;
  Display *disp = XtDisplay(w);

  Newpixmap(w,NULL,NULL);

  XtSetArg(args[0], XtNwidth, &width);
  XtSetArg(args[1], XtNheight, &height);
  XtSetArg(args[2], XtNdepth, &depth);
  XtSetArg(args[3], XtNbackground, &fg);
  XtGetValues(w, args, 4);
  XSetForeground(disp, cw->canvas.gc, fg);
  XFillRectangle(disp, cw->canvas.pixmap, cw->canvas.gc, 0, 0, width, height);
}

void ExposeCanvas(Widget w)
{
  XClearArea(XtDisplay(w), XtWindow(w), 0, 0, 0, 0, True);
}
  
Pixmap CanvasPixmap(Widget w)
{
  return ((CanvasWidget)w)->canvas.pixmap;
}