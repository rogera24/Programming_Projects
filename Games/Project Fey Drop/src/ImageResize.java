import java.awt.Graphics;
import java.awt.Image;
import java.awt.Toolkit;

import javax.swing.Icon;
import javax.swing.JComponent;

class ImageResize extends JComponent {
	int h;
	int w;
	String image;

	ImageResize(int w, int h, String img){
		this.h = h;
		this.w = w;
		this.image = img;
	}
	
	public void paint(Graphics g) {
		Image img1 = Toolkit.getDefaultToolkit().getImage(image);
		
		float scalor = (float)this.w/(float)1600;
		int i = img1.getHeight(this);
		int j = img1.getWidth(this);
		
		g.drawImage(img1, 0, 0, (int) (j*scalor), (int) (i*scalor), this);

	}
}