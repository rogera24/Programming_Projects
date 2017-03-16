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
	int pip[2];
	if (pipe(pip) == -1)
	{
		perror("pipe");
		exit(1);
	}
	char buff[200];
	int p = fork();
	if (p == 0)
	{
		int ppip[2];
		if (pipe(ppip) == -1)
		{
			perror("pipe");
			exit(1);
		}
		int p_id = fork();
		if (p_id == 0)
		{
			//I am child.
			char text[500];
			close(ppip[1]);
			fpipe = fdopen(ppip[0], "r");
			fgets(text, sizeof(text), fpipe);
			close(1);
			dup(pip[1]);
			close(pip[0]);
			execl("grep", "grep", "flowers", NULL);
			perror("excel grep");
			exit(3);
		}
		else
		{
			//I am child's child.
			close(1);
			dup(ppip[1]);
			close(ppip[0]);
			execl("cat", "cat", "poem", "poem1", "poem2", NULL);
			perror("execl");
			exit(3);
		}
	}
	else
	{
		//Hello I am parent
		int n;
		char text[500];
		close(pip[1]);
		fpipe = fdopen(pip[0], "r");
		fgets(buff, sizeof(buff), fpipe);
		printf("%s", buff);
	}
}

