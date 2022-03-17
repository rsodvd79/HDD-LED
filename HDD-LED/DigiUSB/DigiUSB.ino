
#define USB_CFG_DEVICE_NAME     'H','D','D','L','e','d'
#define USB_CFG_DEVICE_NAME_LEN 6

#include <DigiUSB.h>
#include <WS2811.h>

#define MAX_LED 20

DEFINE_WS2811_FN(WS2811RGB, PORTB, 1)
RGB_t rgb[MAX_LED]; //1 for 1 pixel

byte byteCNT = 0;
byte byteLID = 0;
byte byteVAR = 0;
byte byteVAG = 0;
byte byteVAB = 0;

void erasePixel(){
  for (byte i=0;i<MAX_LED;i++) {
    setPixel(i+1,0,0,0);
  }
}

void setPixel(byte i, byte r, byte g, byte b){
  rgb[i - 1].r=r;
  rgb[i - 1].g=g;
  rgb[i - 1].b=b;
}

void updatePixels(){
  WS2811RGB(rgb, ARRAYLEN(rgb));
}

void setup() {
  DigiUSB.begin();
  pinMode(1, OUTPUT); //LED on Model A  or Pro
  erasePixel();
}

void loop() {
  if (DigiUSB.available()) {
    boolean toComplete = false;
    byte lr = DigiUSB.read();
    DigiUSB.write(lr);
    
    switch (byteCNT){
      case 0:
        byteLID = lr;
        if (byteLID == 0 || byteLID > MAX_LED) {
          erasePixel();
          toComplete = true;

        } else {
          byteCNT ++;

        }

      break;
      case 1:
        byteVAR = lr;
        byteCNT ++;      

      break;
      case 2:
        byteVAG = lr;
        byteCNT ++;      

      break;
      case 3:
        byteVAB = lr;
        setPixel(byteLID, byteVAR, byteVAG, byteVAB);
        toComplete = true;

      break;
    }

    if(toComplete){
      updatePixels();

      byteCNT = 0;      
      byteLID = 0;
      byteVAR = 0;
      byteVAG = 0;
      byteVAB = 0;
    }

  } else {
    DigiUSB.delay(10);

  }

}
