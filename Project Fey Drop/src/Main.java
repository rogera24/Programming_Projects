import java.awt.Color;
import java.awt.Dimension;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;

import javax.imageio.ImageIO;
import javax.swing.ImageIcon;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;

import Tests.ThreadTest;

public class Main {

	public static void main(String[] args) throws InterruptedException {
		WorldVar WV = new WorldVar(800);
		Menus.IntroCredits(WV);
	}
}
