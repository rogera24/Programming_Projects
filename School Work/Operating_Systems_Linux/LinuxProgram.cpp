// Linux Program.cpp : Defines the entry point for the console application.
//

#include <fstream>
#include <string>
#include <iostream>
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <sys/types.h> 
#include <unistd.h>
#include <limits.h>


int main(int argc, char **argv, char **env)
{
	int n;
	printf("System Call: The current process ID: %d\n", getpid());
	printf("System Call: The parent Process of this process: %d\n", getppid());

	char buff[PATH_MAX + 1];
	char* cwd;
	cwd = getcwd(buff, PATH_MAX + 1);

	printf("Library Call: The current directory is: %s\n", cwd);
	printf("System Call: The current user id number is: %d\n", getuid());
	printf("System Call: The current effective user id number is: %d\n", geteuid());
	printf("Library Call: The current login user name is: %s\n", getlogin());
	printf("System Call: Changing Dir");
	chdir("/home/baumbach/txt_files");
	
	cwd = getcwd(buff, PATH_MAX + 1);

	printf("Library Call: The current directory is: %s\n", cwd);

	printf("I have %d command line arguments:\n", argc);
	for (n = 0; n < argc; n++)
		printf("	%s\n", argv[n]);

	printf("Hello my name is steve....");
}

