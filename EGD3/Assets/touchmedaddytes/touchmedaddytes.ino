

//cant use 1 or 0


const int seven = 7;
const int four = 4;
const int two=2;
const int three=3;
const int eight=8;
const int eleven=11;
const int twelve=12;
int touched =0;
int touch4=0;
int touch2=0;
int touch3=0;
int touch8=0;
int touch11=0;
int touch12=0;

int past_touched =0;
int past_touch4=0;
int past_touch2=0;
int past_touch3=0;
int past_touch8=0;
int past_touch11=0;
int past_touch12=0;

void setup() {
  // put your setup code here, to run once:
  pinMode(seven,INPUT);
  pinMode(four,INPUT);
  pinMode(two,INPUT);
  pinMode(three,INPUT);
  pinMode(eight,INPUT);
  pinMode(eleven,INPUT);
  pinMode(twelve,INPUT);
  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
    touched = digitalRead(seven);
    touch4 = digitalRead(four);
    touch2 = digitalRead(two);
    touch3 = digitalRead(three);
    touch8 = digitalRead(eight);
    touch11 = digitalRead(eleven);
    touch12 = digitalRead(twelve);
  
    if(touched!= past_touched){
        Serial.write(4);
        Serial.flush();
        past_touched = touched;
        delay(20);
    }
    else if(touch4 != past_touch4){
        Serial.write(3);
        Serial.flush();
        past_touch4 = touch4;
        delay(20);
    }
    else if(touch2!=past_touch2){
        Serial.flush();
        Serial.write(1);
        Serial.flush();
        past_touch2=touch2;
        delay(20);
    }
    else if(touch3!=past_touch3){
        Serial.write(2);
        Serial.flush();
        past_touch3=touch3;
        delay(20);
    }
    else if(touch8!=past_touch8){
        Serial.write(5);
        Serial.flush();
        past_touch8=touch8;
        delay(20);
    }
    else if(touch11!=past_touch11){
        Serial.write(6);
        Serial.flush();
        past_touch11=touch11;
        delay(20);
    }
    else if(touch12!=past_touch12){
        Serial.write(7);
        Serial.flush();
        past_touch12=touch12;
        delay(20);
    }
    else {
      Serial.write(0);
      Serial.flush();
      delay(20);
    }
}
