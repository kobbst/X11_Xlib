
EXTRA_DIST =  Makefile

# Warning: This is an automatically generated file, do not edit!

srcdir=.
top_srcdir=..

include $(top_srcdir)/config.make

ifeq ($(CONFIG),DEBUG_X86)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG;"
ASSEMBLY = bin/Debug/XtWrapper.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../X11Wrapper/bin/Debug/X11Wrapper.dll
BUILD_DIR = bin/Debug

XTWRAPPER_DLL_MDB_SOURCE=bin/Debug/XtWrapper.dll.mdb
XTWRAPPER_DLL_MDB=$(BUILD_DIR)/XtWrapper.dll.mdb
LIBXAWNATIVE_SO_SOURCE=../XawNative/bin/Debug/libXawNative.so
X11WRAPPER_DLL_SOURCE=../X11Wrapper/bin/Debug/X11Wrapper.dll
X11WRAPPER_DLL_MDB_SOURCE=../X11Wrapper/bin/Debug/X11Wrapper.dll.mdb
X11WRAPPER_DLL_MDB=$(BUILD_DIR)/X11Wrapper.dll.mdb

endif

ifeq ($(CONFIG),RELEASE_X86)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+
ASSEMBLY = bin/Release/XtWrapper.dll
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../X11Wrapper/bin/Release/X11Wrapper.dll
BUILD_DIR = bin/Release

XTWRAPPER_DLL_MDB=
LIBXAWNATIVE_SO_SOURCE=../XawNative/bin/Debug/libXawNative.so
X11WRAPPER_DLL_SOURCE=../X11Wrapper/bin/Release/X11Wrapper.dll
X11WRAPPER_DLL_MDB=

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(XTWRAPPER_DLL_MDB) \
	$(LIBXAWNATIVE_SO) \
	$(X11WRAPPER_DLL) \
	$(X11WRAPPER_DLL_MDB)  

LINUX_PKGCONFIG = \
	$(XTWRAPPER_PC)  


RESGEN=resgen2

LIBXAWNATIVE_SO = $(BUILD_DIR)/libXawNative.so
X11WRAPPER_DLL = $(BUILD_DIR)/X11Wrapper.dll
XTWRAPPER_PC = $(BUILD_DIR)/xtwrapper.pc

FILES = \
	Xtlib.cs \
	XtConventions.cs \
	RuntimeContextHelper/CallBackMarshaler.cs \
	RuntimeContextHelper/ActionMarshaler.cs \
	XtWidgetWrapper/XtShell.cs \
	XtWidgetWrapper/XtWmShell.cs \
	XtWidgetWrapper/XtApplicationShell.cs \
	XtWidgetWrapper/XtDialog.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	../XawNative/bin/Debug/libXawNative.so \
	RuntimeContextHelper \
	XtWidgetWrapper \
	xtwrapper.pc.in 

REFERENCES =  \
	System

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

#Targets
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG)  $(top_srcdir)/config.make

include $(top_srcdir)/Makefile.include
#include $(srcdir)/custom-hooks.make



$(eval $(call emit-deploy-target,LIBXAWNATIVE_SO))
$(eval $(call emit-deploy-target,X11WRAPPER_DLL))
$(eval $(call emit-deploy-target,X11WRAPPER_DLL_MDB))
$(eval $(call emit-deploy-wrapper,XTWRAPPER_PC,xtwrapper.pc))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'


$(ASSEMBLY_MDB): $(ASSEMBLY)
$(ASSEMBLY): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	make pre-all-local-hook prefix=$(prefix)
	mkdir -p $(shell dirname $(ASSEMBLY))
	make $(CONFIG)_BeforeBuild
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
	make $(CONFIG)_AfterBuild
	make post-all-local-hook prefix=$(prefix)

install-local: $(ASSEMBLY) $(ASSEMBLY_MDB)
	make pre-install-local-hook prefix=$(prefix)
	make install-satellite-assemblies prefix=$(prefix)
	mkdir -p '$(DESTDIR)$(libdir)/$(PACKAGE)'
	$(call cp,$(ASSEMBLY),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(ASSEMBLY_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(XTWRAPPER_DLL_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(LIBXAWNATIVE_SO),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(X11WRAPPER_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(X11WRAPPER_DLL_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	mkdir -p '$(DESTDIR)$(libdir)/pkgconfig'
	$(call cp,$(XTWRAPPER_PC),$(DESTDIR)$(libdir)/pkgconfig)
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB)
	make pre-uninstall-local-hook prefix=$(prefix)
	make uninstall-satellite-assemblies prefix=$(prefix)
	$(call rm,$(ASSEMBLY),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ASSEMBLY_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(XTWRAPPER_DLL_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(LIBXAWNATIVE_SO),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(X11WRAPPER_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(X11WRAPPER_DLL_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(XTWRAPPER_PC),$(DESTDIR)$(libdir)/pkgconfig)
	make post-uninstall-local-hook prefix=$(prefix)

install: install-local
uninstall: uninstall-local
clean: clean-local

include $(top_srcdir)/rules.make
