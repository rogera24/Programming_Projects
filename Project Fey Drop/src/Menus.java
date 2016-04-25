import java.awt.Color;
import java.awt.Dimension;

import javax.swing.JFrame;
import javax.swing.JLayeredPane;
import javax.swing.JPanel;

public class Menus {
	public static JFrame IntroCredits(WorldVar WV) throws InterruptedException{
		int h = WV.getHeight();
		int w = WV.getWidth();
		ImageResize Producer = new ImageResize(w, h, "Images/LunacyArtsProduction.jpg");
		JFrame window = WV.getFrame();
		window.getContentPane().add(Producer);
		Thread.sleep(2500);
		JPanel panel = new JPanel();
		panel.setBackground(new Color(0, 0, 0, 0));
		window.add(panel);
		window.validate();
		for(int i = 0; i <= 100; i++){
			panel.setBackground(new Color(0, 0, 0, i));
			Thread.sleep(15);
			panel.validate();
		}
		window.remove(panel);
		window.revalidate();
		return window;
	}
}
