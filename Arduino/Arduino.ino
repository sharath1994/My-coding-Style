
/*
Industrial Automation  
 
 
 * Serial2 port is used for Serial2 communication with PC
 ** the status is displayed on PC using Serial2 communication and controlling device status 
 also possible through same application
 
 
 designed  on 05 FEB 2013
 by P.SharathChandra
 	 
 */

// Importing all required libraries 


int LdrPin = A0;        // LDR is connected to A0
int tempPin= A1;        // LM35 Connected to A1
int smokePin  =  A2;    // smoke sensor is connected to A2

int L1 = 4;
int L2 = 5;
int L3 = 6;
int L4 = 7;

int pw1 = 9;
int pw2 = 10;
int pw3 = 11;

int data [25];           // Declaring BYTE array

int IROne = 22;
int IRTwo = 24;
int PIROne = 26;
int LPG = 28;


int dataSend[25];
int val;
void setup()
{
  for(int j = 4;j<=11;j++)
    pinMode(j,OUTPUT);
  pinMode(IROne,INPUT_PULLUP);
  pinMode(IRTwo,INPUT_PULLUP);
  pinMode(PIROne,INPUT_PULLUP);
  pinMode(LPG,INPUT_PULLUP);

  for(int i=1;i<=24;i++)
    data[i]=0;
  data[0]='#';
  data[24]='$';

  Serial2.begin(9600*2);
  Serial1.begin(9600);    // initialising Serial2 communication with 9600 baud rate
}

void loop()
{
  if(Serial1.available()>0){
    char in = Serial1.read();
    if( in =='B'){
      in =Serial1.read();
      Command(in);
    }
  }
  if(Serial2.available()>0){
    char in = Serial2.read();
    if( in =='P'){
      in =Serial2.read();
      Command(in);
    }
  }
  if(true){
    data[8] =  analogRead(A0)/2;
    data[9] =  analogRead(A1)/2;
    data[10] = map(analogRead(A2),0,1000,0,100);
    data[11] = map(analogRead(A3),0,1000,0,100);
    if(digitalRead(LPG)) 
      data[12] = 1;
    else 
      data[12] = 0;
    if(digitalRead(PIROne)) 
      data[13] = 1;
    else 
      data[13] = 0;
    if(digitalRead(IROne)) 
      data[14] = 1;
    else 
      data[14] = 0;
    if(digitalRead(IRTwo)) 
      data[15] = 1;
    else 
      data[15] = 0;
    data[16] = random(20,80);


    Serial2.write('#');
    for(int i=0;i<=24;i++)
      Serial2.write(data[i]);

    //Seding data to Aap
    for(int i=0;i<=24;i++)
      dataSend[i] = data[i];
    for(int i=8;i<18;i++)
      dataSend[i] = data[i];

    Serial1.write('#');
    for(int i=0;i<=24;i++)
      Serial1.write(dataSend[i]);
  }
  Action();
  delay(10);
}


void Action(){
  if(data[1] == 1)
    digitalWrite(L1,HIGH);
  if(data[1] == 0)
    digitalWrite(L1,LOW);
  if(data[2] == 1)
    digitalWrite(L2,HIGH);
  if(data[2] == 0)
    digitalWrite(L2,LOW);
  if(data[3] == 1)
    digitalWrite(L3,HIGH);
  if(data[3] == 0)
    digitalWrite(L3,LOW);
  if(data[4] == 1)
    digitalWrite(L4,HIGH);
  if(data[4] == 0)
    digitalWrite(L4,LOW);
  analogWrite(pw1,(data[5]*25)+1);
  analogWrite(pw2,(data[6]*25)+1);
  analogWrite(pw3,(data[7]*25)+1);
}

void Command(char in){

  if(in >30 && in <40){
    in = in - 30;
    data[5] = in;
    dataSend[5] = in;
    in = (in*25)+1;
    analogWrite(pw1,in);
  }
  if(in>20&&in < 30){
    in = in - 20;
    data[6] = in;
    dataSend[6] = in;
    in = (in*25);
    analogWrite(pw2,in);
  }
  if(in>10&&in < 20){
    in = in - 10;
    data[7] = in;
    dataSend[7] = in;
    in = (in*25);
    analogWrite(pw3,in);
  }

  switch(in)
  {
    //case '0':
  case 0:
    data[1] =1;
    digitalWrite(L1,HIGH);
    delay(30);
    Serial2.write('M');
    // Serial2.write('+');
    Serial2.write('1');
    break;
    //case '1':
  case 1:
    data[1] = 0;
    digitalWrite(L1,LOW);
    delay(30);
    Serial2.write('M');
    // Serial2.write('+');
    Serial2.write('2');
    break;
    //case '2':
  case 2:
    data[2] = 1;
    digitalWrite(L2,HIGH);
    Serial2.write('M');
    // Serial2.write('+');
    Serial2.write('3');
    break;
    //case '3':
  case 3:
    data[2] = 0;
    digitalWrite(L2,LOW);
    Serial2.write('M');
    // Serial2.write('+');
    Serial2.write('4');
    break;
    // case '4':
  case 4:
    data[3] = 1;
    digitalWrite(L3,HIGH);
    Serial2.write('M');
    // Serial2.write('+');
    Serial2.write('5');
    break;
    //case '5':
  case 5:
    data[3] = 0;
    digitalWrite(L3,LOW);
    Serial2.write('M');
    // Serial2.write('+');
    Serial2.write('6');
    break;

    //case '6':
  case 6:
    data[4] = 1;
    digitalWrite(L4,HIGH);
    Serial2.write('M');
    // Serial2.write('+');
    Serial2.write('7');
    break;
    //case '7':
  case 7:
    data[4] = 0;
    digitalWrite(L4,LOW);
    Serial2.write('M');
    // Serial2.write('+');
    Serial2.write('8');
    break;
  } 
}

