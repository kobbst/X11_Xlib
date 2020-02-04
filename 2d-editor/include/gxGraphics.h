/**
** 20 Graphical Editor (2d -gx)
**
** gxGraphics.h
*/
#include <X11/Intrinsic.h>

#ifndef _GX_GRAPHICS_H_INC_
#define _GX_GRAPHICS_H_INC_

#endif /* _GX_GRAPHICS_H_INC_ */

#ifndef GLOBAL
#define GLOBAL
#else
#undef GLOBAL
#define GLOBAL extern
#endif

typedef struct _gxline {
    XPoint *pts;
    int     num_pts;
} GXLine, *GXLinePtr;

typedef XPoint ***GXFont;
typedef int    ** GXFontP;

typedef XArc GXArc;
typedef XArc *GXArcPtr;



typedef struct _gxtext {
    int x, y;
    /* upper-left */
    int dx, dy;
    char *text;
    int len;
    GXFont vpts;
    GXFont font;
    GXFontP fontp;
} GXText, *GXTextPtr;

typedef struct _gx_obj {
    /*
    * attributes
    */
    Pixel fg; /* foreground */
    Pixel bg; /* background */

    Pixmap fs; /* fill style */
    int     ls; /* line style */
    int     lw; /* line width */

    Boolean selected;
    XRectangle *handles;
    int     num_handles;

    void *data;

    void (*draw) ( struct _gx_obj * );
    void (*erase) ( struct _gx_obj * );

    XRectangle *(*bounds)( struct _gx_obj * );
    Boolean (*find) ( struct _gx_obj *, XEvent * );

    void (*select) ( struct _gx_obj * );
    void (*deselect) ( struct _gx_obj * );
    void (*copy) ( struct _gx_obj * );

    void (*move) ( struct _gx_obj *, XEvent * );
    void (*scale) ( struct _gx_obj *, XEvent * );
    void (*action) ( struct _gx_obj *, XEvent * );
    /*
    * link to other objects
    */
    struct _gx_obj *next;
 } GXObj, *GXObjPtr;

GLOBAL Widget GxStatusBar;
GLOBAL Widget GxDrawArea;

GLOBAL Widget GxStatusBar;
GLOBAL Widget GxDrawArea;
GLOBAL Pixmap GxDrawAreaBG;
GLOBAL GC rubberGc;
GLOBAL int GxActiveHandle;
GLOBAL GXObjPtr gxObjHeader;
GLOBAL GXObjPtr gxObjCurrent;

GLOBAL int FixedX, FixedY;
GLOBAL int OrigX, OrigY;
GLOBAL int ExntX, ExntY;


typedef enum _cursor_mode {
        NORMAL_MODE = 0,
        PENCIL_MODE,
        EDIT_MODE,
        TEXT_MODE,
        MOVE_MODE,
        SCALE_MODE,
        LINE_MODE
} CursorMode;

/**
** end of gxGraphics.h
*/