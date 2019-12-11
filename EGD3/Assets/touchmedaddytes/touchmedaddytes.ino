

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

    String total="0";
    if(touched){
        total+="4";
    }
    if(touch4){
        total+="3";
    }
    if(touch2){
        total+="1";
    }
    if(touch3){
        total+="2";
    }
    if(touch8){
        total+="5";
    }
    if(touch11){
        total+="6";
    }
    if(touch12){
        total+="7";
    }
    Serial.println(total);
    Serial.flush();
    delay(20);
}
