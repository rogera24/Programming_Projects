package Tests;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;

import javax.imageio.ImageIO;
import javax.swing.ImageIcon;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;

public class ImgThread2 implements Runnable {
	private Thread t;
	private String threadName;
	private JFrame win;
	private JPanel panel2;

	public ImgThread2( String name, JFrame win, JPanel panel2){
		threadName = name;
		System.out.println("Creating " +  threadName );
		this.win = win;
		this.panel2 = panel2;
	}

	@Override
	public void run() {
		BufferedImage image2;
		JLabel label2;
		try {
			image2 = ImageIO.read(new File("Images/Zero1.png"));
			label2 = new JLabel(new ImageIcon(image2));
			label2.setLocation(0, 0);
			panel2.add(label2);
		} catch (IOException e1) {
			// TODO Auto-generated catch block
			System.out.println("Error Loading Initial Image!");
			return;
		}
		String images[] = {"Zero1.png","Zero2.png","Zero3.png","Zero4.png","Zero5.png","Zero6.png"};
		while(true){
			for(int i = 0; i < 6; i++){
				try {
					image2 = ImageIO.read(new File("Images/" + images[i]));
					label2.setIcon(new ImageIcon(image2));
				} catch (IOException e) {
					System.out.println("Image not Found!!");
				}
				label2.validate();
				win.validate();
				try {
					Thread.sleep(50);
				} catch (InterruptedException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			}
		}
	}

	public void start()
	{
		t = new Thread (this, threadName);
		t.start ();
	}
}