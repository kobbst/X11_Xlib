//
// example5.cpp
//
#include <X11/Xlib.h>
#include <iostream>
#include "xlib++/display.hpp"
#include "xlib++/window.hpp"
#include "xlib++/graphics_context.hpp"
using namespace xlib;


class main_window : public window
{
 public:
  main_window ( event_dispatcher& e ) : window ( e ) {};
  ~main_window(){};

  void on_expose ()
  {
    graphics_context gc ( get_display(), id() );

    gc.draw_line ( line ( point(0,0), point(150,150) ) );
    gc.draw_line ( line ( point(150,150), point(250,50) ) );
    gc.set_background(20,255,20);
    gc.draw_rectangle(rectangle (point(0,0),100, 100));
    
    gc.set_lineAttributes(10, LineSolid, CapRound, JoinMiter);
    gc.set_foreground(55,0,10);
    gc.fill_rectangle(rectangle (point(100,0),100, 100));
    // gc.draw_text ( point(0, 70), "I'm drawing!!" );
  }

};

int main()
{
  try
    {
      // Open a display.
      display d("");

      event_dispatcher events ( d );
      main_window w ( events ); // top-level
      events.run();
    }
  catch ( std::exception& e )
    {
      std::cout << "Exception: " << e.what() << "\n";
    }
  return 0;
}
