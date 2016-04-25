package Tests;
import java.awt.Dimension;

import javax.swing.JFrame;
import javax.swing.JPanel;

public class ThreadTest {
	public void Threadtest(){
		System.out.println("Starting Thread Example");
	}
	public void start() throws InterruptedException{
		JFrame win = new JFrame("Multi-Thread Test");
		win.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		win.setVisible(true);
		Dimension dim = new Dimension(320, 180);
		win.setPreferredSize(dim);
		JPanel panel = new JPanel();
		panel.setLocation(0,0);
		panel.setSize(100, 100);
		win.add(panel);
		JPanel panel2 = new JPanel();
		panel2.setLocation(200,0);
		panel2.setSize(100, 100);
		win.add(panel2);
		win.pack();
		ImgThread T1 = new ImgThread("ImgThread", win, panel);
		T1.start();
		ImgThread2 T2 = new ImgThread2("ImgThread2", win, panel2);
		T2.start();
		return;
	}
}
