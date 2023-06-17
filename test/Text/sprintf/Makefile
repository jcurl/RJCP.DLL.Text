SRCS = testcaseblock.cpp testcasegen.cpp
OBJS = $(SRCS:.cpp=.o)

CPP := $(or $(CPP),g++)
CPPFLAGS := $(or $(CPPFLAGS),-Wall)

all: sprintf

$(OBJS): %.o : %.hpp

%.o: %.cpp
	g++ $(CPPFLAGS) -c $<

sprintf: $(OBJS) sprintf.cpp
	g++ $(CPPFLAGS) $^ -o $@

.PHONY: clean
clean:
	rm -f $(OBJS) sprintf
