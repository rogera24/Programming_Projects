#include <iostream>
#include <csignal>
#include <fstream>
#include <string>
#include <iostream>
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <sys/types.h> 
#include <unistd.h>
#include <limits.h>
using namespace std;

int main(int argc, char **argv, char **env)
{
	int i = (int)argv[0];
	while (1){
		kill(i, SIGINT);
		sleep(10);
	}
}