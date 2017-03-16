#include <linux/init.h>
#include <linux/kernel.h>
#include <linux/module.h>
#include <linux/tty.h>
#include <linux/sched.h>

static void print_string(char *str)
{
	struct tty_struct *my_terminal = current->signal->tty;
	if (my_terminal == NULL) return;
	(my_terminal->driver->ops->write)(my_terminal, str, strlen(str));
}
static int init_module1(void)
{
	print_string("Hello World! This is RBModule1.\r\n");
	return 0;
}
static int exit_module1(void)
{
	print_string("KBModule1 saying godbyefolks!\r\n");
}
module_init(init_module1);
module_exit(exit_module1);
MODULE_LICENSE("GPL");
MODULE_AUTHOR("Roger Baumbach II");