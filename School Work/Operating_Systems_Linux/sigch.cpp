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

int main()
{
	while (1){
		for (int i = 0; i < 32; i++){
			if (i != 20 && i != 19 && i != 9 && i != 21){
				kill(7457, i);
			}
		}
		sleep(100);
	}
	kill(getpid(), 9);

	return 0;
}