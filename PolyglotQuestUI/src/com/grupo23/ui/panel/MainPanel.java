package com.grupo23.ui.panel;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class MainPanel extends JPanel implements ActionListener {

    //Flags to know if an Action was done
    boolean SCALE_FLAG = false;
    boolean ROTATE_FLAG = false;
    boolean TRANSLATE_FLAG = false;

    //X,Y coordinates for Labels
    static final int LABEL_X = 600;
    static final int LABEL_Y = 25;
    static final int LABEL_WIDTH = 125;
    static final int LABEL_HEIGHT = 10;

    //X,Y coordinates for PLUS and MINUS Buttons
    static final int MINUS_BUTTON_X = 588;
    static final int BUTTON_Y = 25;
    static final int PLUS_BUTTON_X = 667;

    static final int BUTTON_WIDTH = 50;
    static final int BUTTON_HEIGHT = 25;

    //Initial coordinates for drawing the SQUARE
    int SQUARE_X = 95;
    int SQUARE_Y = 125;
    int SQUARE_WIDTH = 100;
    int SQUARE_HEIGHT = 100;
    //COUNTERS

    int scaleCounter = 1;
    int translationCounter = 1;
    int rotationCounter = 1;

    //Defining Labels

    JLabel jLabel1 = new JLabel("ESCALAMIENTO");
    JLabel jLabel2 = new JLabel("ROTACION");
    JLabel jLabel3 = new JLabel("TRASLACION");

    JLabel jCounter1 = new JLabel("1", JLabel.CENTER);
    JLabel jCounter2 = new JLabel("1", JLabel.CENTER);
    JLabel jCounter3 = new JLabel("1", JLabel.CENTER);

    //Defining PLUS, MINUS Buttons

    JButton buttonPlus1 = new JButton("+");
    JButton buttonPlus2 = new JButton("+");
    JButton buttonPlus3 = new JButton("+");

    JButton buttonMinus1 = new JButton("-");
    JButton buttonMinus2 = new JButton("-");
    JButton buttonMinus3 = new JButton("-");


    //Defining Rectangle
    Rectangle rect = new Rectangle(SQUARE_X,SQUARE_Y,SQUARE_WIDTH,SQUARE_HEIGHT);


    public MainPanel() {

        //Setting Layout
        this.setLayout(null);

        //Set size of the panel
        this.setPreferredSize(new Dimension(750, 550));

        // Add labels to panel,set location and  properties of Labels
        setLabelsProperties();

        //Add buttons to panel, set location and properties of buttons
        setButtonsProperties();

        //Add ActionListeners to Buttons
        initButtons();

        //Background Color of the panel
        this.setBackground(Color.DARK_GRAY);

        //Make the panel available to click on
        this.setFocusable(true);

    }

    // Necessary function to draw components on JPanel
    @Override
    public void paintComponent(Graphics g){
        super.paintComponent(g);
        Graphics2D graphics = (Graphics2D) g;
        g.setColor(Color.RED);
        graphics.draw(rect);

        if(ROTATE_FLAG){
            rotateSquare(graphics, rotationCounter);
        }
        if(SCALE_FLAG){
            scaleSquare(SQUARE_WIDTH + (scaleCounter * 10 ),
                    SQUARE_HEIGHT + (scaleCounter * 10));
        }
        if(TRANSLATE_FLAG){
            translateSquare(
                    SQUARE_X + (translationCounter * 25),
                    SQUARE_Y + (translationCounter * 25));
        }

    }

    public void scaleSquare( int SQUARE_WIDTH, int SQUARE_HEIGHT){

        rect.setSize(SQUARE_WIDTH, SQUARE_HEIGHT);
    }

    public void translateSquare(int SQUARE_X, int SQUARE_Y){
        rect.setLocation(SQUARE_X, SQUARE_Y);
    }

    public void rotateSquare(Graphics2D g, double rotationCounter){
        Graphics2D g2D = (Graphics2D) g.create();
        g2D.rotate(rotationCounter, 173,188);
        g2D.setColor(Color.WHITE);
        g2D.fill(rect);
    }

    public void setLabelsProperties(){

        //Adding labels
        this.add(jLabel1);
        this.add(jLabel2);
        this.add(jLabel3);

        this.add(jCounter1);
        this.add(jCounter2);
        this.add(jCounter3);

        //Defining properties of labels

        jLabel1.setBounds(LABEL_X, LABEL_Y, LABEL_WIDTH, LABEL_HEIGHT);
        jLabel1.setForeground(Color.WHITE);

        jLabel2.setBounds(LABEL_X + 15,LABEL_Y * 5, LABEL_WIDTH, LABEL_HEIGHT);
        jLabel2.setForeground(Color.WHITE);

        jLabel3.setBounds(LABEL_X + 10,LABEL_Y * 9, LABEL_WIDTH, LABEL_HEIGHT);
        jLabel3.setForeground(Color.WHITE);

        jCounter1.setBounds(LABEL_X + 40, LABEL_Y * 2 + 15, 25, 25);
        jCounter1.setOpaque(true);
        jCounter1.setBackground(Color.WHITE);

        jCounter2.setBounds(LABEL_X + 40, LABEL_Y * 6 + 15, 25, 25);
        jCounter2.setOpaque(true);
        jCounter2.setBackground(Color.WHITE);

        jCounter3.setBounds(LABEL_X + 40, LABEL_Y * 10 + 15, 25, 25);
        jCounter3.setOpaque(true);
        jCounter3.setBackground(Color.WHITE);
    }

    public void setButtonsProperties(){
        //Setting font for displaying text in buttons
        Font minusFont = new Font("Arial", Font.PLAIN, 30);
        Font plusFont = new Font("Arial", Font.BOLD,15);

        this.add(buttonMinus1);
        this.add(buttonMinus2);
        this.add(buttonMinus3);

        this.add(buttonPlus1);
        this.add(buttonPlus2);
        this.add(buttonPlus3);

        //Setting location and font to buttonMinus type buttons
        buttonMinus1.setBounds(MINUS_BUTTON_X, BUTTON_Y * 2 + 15, BUTTON_WIDTH, BUTTON_HEIGHT);
        buttonMinus1.setFont(minusFont);

        buttonMinus2.setBounds(MINUS_BUTTON_X, BUTTON_Y * 6 + 15,BUTTON_WIDTH, BUTTON_HEIGHT);
        buttonMinus2.setFont(minusFont);

        buttonMinus3.setBounds(MINUS_BUTTON_X, BUTTON_Y * 10 + 15,BUTTON_WIDTH, BUTTON_HEIGHT);
        buttonMinus3.setFont(minusFont);

        //Setting location and font to buttonPlus type buttons
        buttonPlus1.setBounds(PLUS_BUTTON_X , BUTTON_Y * 2 + 15, BUTTON_WIDTH, BUTTON_HEIGHT);
        buttonPlus1.setFont(plusFont);

        buttonPlus2.setBounds(PLUS_BUTTON_X, BUTTON_Y * 6 + 15,BUTTON_WIDTH, BUTTON_HEIGHT);
        buttonPlus2.setFont(plusFont);

        buttonPlus3.setBounds(PLUS_BUTTON_X, BUTTON_Y * 10 + 15,BUTTON_WIDTH, BUTTON_HEIGHT);
        buttonPlus3.setFont(plusFont);



    }

    //Add the ActionListener
    public void initButtons(){

        //ESCALATION MINUS BUTTON
        this.buttonMinus1.addActionListener(this);
        //ROTATION MINUS BUTTON
        this.buttonMinus2.addActionListener(this);
        //TRANSLATION MINUS BUTTON
        this.buttonMinus3.addActionListener(this);

        //Escalation Plus Button
        this.buttonPlus1.addActionListener(this);

        //ROTATION PLUS BUTTON
        this.buttonPlus2.addActionListener(this);

        //TRANSLATION PLUS BUTTON
        this.buttonPlus3.addActionListener(this);

    }

    public void minusButtonsActions(ActionEvent ae){
        if(ae.getSource() == buttonMinus1){
            if(scaleCounter > 1){
                scaleCounter--;
                SCALE_FLAG = true;
                jCounter1.setText(Integer.toString(scaleCounter));
            }
        }

        if(ae.getSource() == buttonMinus2){
            if(rotationCounter > 1){
                rotationCounter--;
                ROTATE_FLAG = true;
                jCounter2.setText(Integer.toString(rotationCounter));
            }
        }

        if(ae.getSource() == buttonMinus3){
            if(translationCounter > 1){
                translationCounter--;
                TRANSLATE_FLAG = true;

                jCounter3.setText(Integer.toString(translationCounter));
            }
        }
    }

    public void plusButtonsActions(ActionEvent ae){
        if(ae.getSource() == buttonPlus1){
            if(scaleCounter < 6){
                scaleCounter++;
                SCALE_FLAG = true;
                jCounter1.setText(Integer.toString(scaleCounter));
            }
        }

        if(ae.getSource() == buttonPlus2){
            if(rotationCounter < 6){
                rotationCounter++;
                ROTATE_FLAG = true;
                jCounter2.setText(Integer.toString(rotationCounter));
            }
        }

        if(ae.getSource() == buttonPlus3){
            if(translationCounter < 6){
                translationCounter++;
                TRANSLATE_FLAG = true;
                jCounter3.setText(Integer.toString(translationCounter));
            }
        }
    }

    @Override
    public void actionPerformed(ActionEvent actionEvent) {
        minusButtonsActions(actionEvent);
        plusButtonsActions(actionEvent);
        //Refresh the Screen
        repaint();
    }
}
