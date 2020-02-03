/**
** 2D Graphical Editor (2d-gx)
**
** gxLine.c
*/
#include <stdio.h>
#include <X11/Intrinsic.h>

void gx_line( XEvent *event )
{
    printf( “draw a line...\n” );
}

void gx_pencil( XEvent *event )
{
    printf( “draw freestlye\n” );
}
void gx_arrow( XEvent *event )
{
    printf( “draw an arrow\n” );
}
void gx_box( XEvent *event )
{
    printf( “draw a box\n” );
}

/**
** end of gxLine.c
*/