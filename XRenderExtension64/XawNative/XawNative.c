#include <stdlib.h>
#include <stdio.h>
#include <string.h>

#include <X11/Xlib.h>

#include <X11/Intrinsic.h>
#include <X11/IntrinsicP.h>
#include <X11/StringDefs.h>
#include <X11/Core.h>
#include <X11/ShellP.h>

#include <X11/Xaw/AsciiText.h>
#include <X11/Xaw/Box.h>
#include <X11/Xaw/Command.h>
#include <X11/Xaw/Dialog.h>
#include <X11/Xaw/Form.h>
#include <X11/Xaw/Grip.h>
#include <X11/Xaw/Label.h>
#include <X11/Xaw/MenuButton.h>
#include <X11/Xaw/Paned.h>
#include <X11/Xaw/Scrollbar.h>
#include <X11/Xaw/Simple.h>
#include <X11/Xaw/SimpleMenu.h>
#include <X11/Xaw/Sme.h>
#include <X11/Xaw/SmeBSB.h>
#include <X11/Xaw/Text.h>
#include <X11/Xaw/Tree.h>
#include <X11/Xaw/Viewport.h>
#include "MoreWidgets/Canvas.h"

//Provide access to the widget's class record from C#.
void* XawAsciiTextWidgetClass ()
{	return (void*) asciiTextWidgetClass;	}

void* XawBoxWidgetClass ()
{	return (void*) boxWidgetClass;	}

void* XawCanvasWidgetClass ()
{	return (void*) canvasWidgetClass;	}

void* XawCommandWidgetClass ()
{	return (void*) commandWidgetClass;	}

void* XawDialogWidgetClass ()
{	return (void*) dialogWidgetClass;	}

void* XawFormWidgetClass ()
{	return (void*) formWidgetClass;	}

void* XawGripWidgetClass ()
{	return (void*) gripWidgetClass;	}

void* XawLabelWidgetClass ()
{	return (void*) labelWidgetClass;	}

void* XawMenuButtonWidgetClass ()
{	return (void*) menuButtonWidgetClass;	}

void* XawOverrideShellWidgetClass ()
{	return (void*) overrideShellWidgetClass;	}

void* XawPanedWidgetClass ()
{	return (void*) panedWidgetClass;	}

void* XawScrollbarWidgetClass ()
{	return (void*) scrollbarWidgetClass;	}

void* XawSimpleWidgetClass ()
{	return (void*) simpleWidgetClass;	}

void* XawSimpleMenuWidgetClass ()
{	return (void*) simpleMenuWidgetClass;	}

void* XawSmeWidgetClass ()
{	return (void*) smeObjectClass;	}

void* XawSmeBSBWidgetClass ()
{	return (void*) smeBSBObjectClass;	}

void* XawTextWidgetClass ()
{	return (void*) textWidgetClass;	}

void* XawTransientShellWidgetClass ()
{	return (void*) transientShellWidgetClass;	}

void* WrapXawViewportWidgetClass ()
{	return (void*) viewportWidgetClass;	}