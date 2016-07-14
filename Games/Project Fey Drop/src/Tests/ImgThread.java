package Tests;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;

import javax.imageio.ImageIO;
import javax.swing.ImageIcon;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;

public class ImgThread implements Runnable {
	private Thread t;
	private String threadName;
	private JFrame win;
	private JPanel panel;
	
	public ImgThread( String name, JFrame win, JPanel panel){
		threadName = name;
		System.out.println("Creating " +  threadName );
		this.win = win;
		this.panel = panel;
	}

	@Override
	public void run() {
		BufferedImage image;
		JLabel label;
		try {
			image = ImageIO.read(new File("Images/Zero1.png"));
			label = new JLabel(new ImageIcon(image));
			label.setLocation(0, 0);
			panel.add(label);
		} catch (IOException e1) {
			// TODO Auto-generated catch block
			System.out.println("Error Loading Initial Image!");
			return;
		}
		String images[] = {"Zero1.png","Zero2.png","Zero3.png","Zero4.png","Zero5.png","Zero6.png"};
		while(true){
			for(int i = 0; i < 6; i++){
				try {
					image = ImageIO.read(new File("Images/" + images[i]));
					label.setIcon(new ImageIcon(image));
				} catch (IOException e) {
					System.out.println("Image not Found!!");
				}
				label.validate();
				win.validate();
				try {
					Thread.sleep(150);
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