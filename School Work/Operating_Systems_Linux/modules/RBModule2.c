#include <linux/init.h>
#include <linux/kernel.h>
#include <linux/module.h>
#include <linux/tty.h>
#include <linux/sched.h>
#include <linux/proc_fs.h>
#include <linux/seq_file.h>

static int counter = 0;

static int RB_proc_show(struct seq_file *M, void *v)
{
	counter = counter + 1;
	seq_printf(m, "%d) Hello from RBmodule2!\n", counter);
}
static int RB_proc_open(struct inode *inode, struct file *file)
{
	return single_open(file, RB_proc_show, NULL);
}
static void print_string(char *str)
{
	struct tty_struct *my_terminal = current->signal->tty;
	if (my_terminal == NULL) return;
	(my_terminal->driver->ops->write)(my_terminal, str, strlen(str));
}
static const struct file_operations RB_proc_fops =
{
	.owner = THIS_MODULE,
	.open = RB_proc_open,
	.read = seq_read,
	.llseek = seq_lseek,
	.release = single_release
};
static int init_module1(void)
{
	print_string("Hello World! This is RBModule1.\r\n");
	proc_create("RBModule2", 0, NULL, &RB_proc_fops);
	return 0;
}
static int exit_module1(void)
{
	print_string("KBModule1 saying godbyefolks!\r\n");
	remove_proc_entry("RBMOdule2", NULL);
}
module_init(init_module1);
module_exit(exit_module1);
MODULE_LICENSE("GPL");
MODULE_AUTHOR("Roger Baumbach II");