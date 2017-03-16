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

void signalHandler(int signum)
{
	if (signum == 1){
		cout << signum;
	}
	else if (signum == 21){}
	else{
		cout << ", " << signum;
	}
	//signal(SIGINT, signalHandler);
	// cleanup and close up stuff here  
	// terminate program  
}

int main()
{
	// register signal SIGINT and signal handler  
	for (int i = 0; i < 32; i++){
		if (i != 20){
			signal(i, signalHandler);
		}
	}
	cout << "Interrupt signal (";
	for (int i = 0; i < 32; i++){
		if (i != 9 && i != 19 && i != 20){
			kill(getpid(), i);
		}
	}
	cout << ") recieved.\n";

	cout << "Start Listener\n";
	
	char* panda;
	
	cin >> panda;

	kill(getpid(), 9);

	return 0;
}