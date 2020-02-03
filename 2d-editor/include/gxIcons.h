/**
** 2D Graphical Editor (2d-gx)
**
** gxIcons.h
*/

#ifndef _GX_ICONS_H_INC_
#define _GX_ICONS_H_INC_

#include "gxProtos.h"

/*
* Storage for pertinent XBM data
*/

typedef struct _xbm_data {
    unsigned char *bits;
    int w, h;
} XbmData, *XbmDataPtr;

/*
* IconData necessary to create icon
*/
typedef struct _gx_icon_data {
    XbmDataPtr info;

    void (*func)(void);
    char *mesg;
} GxIconData, *GxIconDataPtr;

#define icon_static( name, bits, width, height ) \
static XbmData name = { bits, width, height }


/* prototypes */
extern void create_icons ( Widget, GxIconDataPtr,
                          void (*)(Widget, XtPointer, XtPointer ));
extern Pixmap create_pixmap ( Widget, XbmDataPtr );

#endif /* _GX_ICONS_H_INC_ */

/**
** end of gxIcons.h
*/