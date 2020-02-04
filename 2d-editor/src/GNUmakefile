#
# Makefile for 2d-gx
#
# See the text for a discussion of this file and its syntax

# Read in the system specific environment configuration
include ../make.defines
PROGRAM = 2d-gx
LIBS = -lXaw -lXt -lX11
CFLAGS = $(OPTS) $(INCS)
OBJS = gxMain.o \
gxGraphics.o \
gxLine.o \
gxText.o \
gxArc.o \
gxGx.o
make-target: $(PROGRAM)
$(PROGRAM): $(OBJS)
@echo “Building $(PROGRAM) for $(TARGET)...“
$(CC) -o $(PROGRAM) $(OBJS) $(X11LIB) $(LIBS)
@echo “done"
#
# end of Makefile for 2d-gx
#