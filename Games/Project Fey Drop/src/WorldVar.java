import java.awt.Dimension;

import javax.swing.JFrame;

public class WorldVar {
	
	JFrame FeyDropWindow;
	int Window_H;
	int Window_W;

	WorldVar(int w){
		this.createFeyDropWindow(w);
		return;
	}
	WorldVar(JFrame frame){
		this.setFeyDropWindow(frame);
		return;
	}
	
	public JFrame getFrame(){
		return this.FeyDropWindow;
	}
	public int getHeight(){
		return this.Window_H;
	}
	public int getWidth(){
		return this.Window_W;
	}
	
	public void createFeyDropWindow(int w){
		this.Window_W = w;
		this.Window_H = (int)(w*9)/16;
		this.FeyDropWindow = new JFrame("Project Fey Drop");
		this.FeyDropWindow.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		this.FeyDropWindow.setResizable(false);
		this.FeyDropWindow.setVisible(true);
		Dimension dim = new Dimension(this.Window_W, this.Window_H);
		this.FeyDropWindow.setPreferredSize(dim);
		this.FeyDropWindow.pack();
		return;
	}
	
	public void setFeyDropWindow(JFrame frame){
		FeyDropWindow = frame;
		this.Window_H = frame.getHeight();
		this.Window_W = frame.getWidth();
		return;
	}
	
	public void addVisual(int pane, int component, int x, int y, int h, int w){
		System.out.println("You have created an element in Pane: " + pane + " component: " + component + " at point " + x + ", " + y + " with dimension: " + w + ":" + h + ".");
	}
}