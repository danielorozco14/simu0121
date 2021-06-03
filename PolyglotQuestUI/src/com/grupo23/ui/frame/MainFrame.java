package com.grupo23.ui.frame;

import com.grupo23.ui.panel.MainPanel;

import javax.swing.*;
import java.awt.*;

public class MainFrame extends JFrame {


    public MainFrame() throws HeadlessException {
        //We create a MainPanel Object, an add it to the frame
        MainPanel mainPanel = new MainPanel();

        //We add the mainPanel object to the frame
        this.add(mainPanel);

        //Title of the window (Frame)
        this.setTitle("Metodo de los Elementos Finitos 2D - G23");

        //Close Strategy
        this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

        //Prevent resizing of the window(frame)
        this.setResizable(false);



        //We call this function, to pack everything in the object and create de Frame
        this.pack();



        //Make the frame visible
        this.setVisible(true);

        //To center the window at the center of the screen
        this.setLocationRelativeTo(null);


    }
}
