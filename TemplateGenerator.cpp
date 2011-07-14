#include "TemplateGenerator.h"

using namespace std;

int main (int argc, char** argv)
  {
    if (argc != 2)
      cout << "Usage:\r\n" << argv[0] << " filename\r\n";

    fstream template_stream(argv[1], fstream::in);
    template_stream.close();
  }
