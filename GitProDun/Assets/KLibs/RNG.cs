using UnityEngine;
using System.Collections;

public class RNG {
        double Xip = 0.5;//serves as X(i-1)
        double seed = 0.5;
        double Xi =0.5;//after rand() has the X(i) value
		double Xnorm=0.5;//has the X(i) in 0-1 range

        /// <summary>
        /// Computes Random Number Xi (0-32767), and returns integer
        /// </summary>
        /// <returns></returns>
        public int Rand(){
            Xip = Xi;
			Xi = (25171 * Xip + 13841) % 32767 ;
			Xnorm = Xi/32767;
			return (int)Xi;
		}

        /// <summary>
        /// Computes Random Number Xi (0-32767), and returns corresponding Normalized value
        /// </summary>
        /// <returns></returns>
        public double Randf()
        {
            Xip = Xi;
            Xi = (25171 * Xip + 13841) % 32767;
            Xnorm = Xi / 32767;
            return Xnorm;
        }

    //returns a Random Number between the range a-b
    public double Range(double a, double b){
			return a + (b-a) * Rand();
		}

		//initialises the Seed number
		public void Seed(double prevX){
			Xip = prevX;
            Xi = prevX;
            seed = prevX;
         }

		//returns the last RN
		public double GetX(){
			return Xi;
		}

		//returns the last normalised RN
		public double GetXnorm(){
			return Xnorm;
		}

        //returns the Seed RNG
        public double GetSeed()
        {
            return seed;
        }
}//class end

public class clsShuffler{

	public int[] arrNumber = new int[25];

	public void shuffle(){
		int i,j,k;
		bool[] flgAlotted = new bool[25];
		RNG RGen = new RNG();
		RGen.Seed( (double) Random.Range(0,32000) );

		for(i=0;i<flgAlotted.Length;i++)
			flgAlotted[i]=false;
		
		for(i=0;i< arrNumber.Length; i++){
			k=0;
			j= Mathf.FloorToInt( (float) RGen.Range(0,25) );

			while( flgAlotted[j] ){
				j= Mathf.FloorToInt( (float) RGen.Range(0,25) );
				k++;
				if(k>3)	break;
			}

			if( flgAlotted[j] ){
				for(j=0;j< flgAlotted.Length;j++){
					if( !flgAlotted[j])
						break;
				}
			}

			flgAlotted[j]=true;
			arrNumber[i] = j;
		}
	
	}
}