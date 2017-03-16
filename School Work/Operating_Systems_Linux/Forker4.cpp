#include <fstream>
#include <string>
#include <iostream>
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <sys/types.h> 
#include <unistd.h>
#include <limits.h>

int main(){
	
	int pid;

	printf("I am parent %d\n", getpid());

	for (int i = 0; i <= 3; i++){

		pid = fork();

		if (pid < 0)
		{
			exit(1);
		}
		if (pid == 0)
		{
			printf("I am child %d, with parent %d\n", getpid(), getppid());
			return 0;
		}
	}
	return 0;
}