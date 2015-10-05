#include<stdio.h>
main()
{
int i,j,syn[10][10],n,conf[10],cons[10][10],rule[10][10],sv[10],dv[10],tv[10],temp[10][10],k,prod[10],delay[10],change[10],active[10],c=0,a,b,p=0,t=0,hv[10];
int halt()
{
int h1=0,h2=0;
for(i=0;i<n;i++)
	{
	if (rule[i][0]>conf[i])
		{
		h1=h1+1;
		}
	}
for(i=0;i<n;i++)
	{
		hv[i]=dv[i];
	}
for(i=0;i<n;i++)
	{
	if (hv[i]>0)
		{
		hv[i]=hv[i]-1;
		}
	}
for(i=0;i<n;i++)
	{
	if(hv[i]==0)
		{
			h2=h2+1;
		}
	}
			
if((h1==n)&&(h2==n))
return 0;
else
return 1;	
}
printf("enter the number of nuerons");
scanf("%d",&n);
printf("enter synapse matrix");
for(i=0;i<n;i++)
	{
		for(j=0;j<n;j++)
		scanf("%d",&syn[i][j]);	
	}

printf("enter initial config");
for(i=0;i<n;i++)
scanf("%d",&conf[i]);

printf("enter %d rules :lhs rhs delay ",n);
for(i=0;i<n;i++)
	{
		for(j=0;j<3;j++)
		scanf("%d",&rule[i][j]);
	}


for(i=0;i<n;i++)		//consumption matrix
	{
		for(j=0;j<n;j++)
		if(i==j)
			{
				cons[i][j]=(-rule[i][0]);
			}
			else
				{
					cons[i][j]=0;	

				}
	}
for(i=0;i<n;i++)		//producing and delay
	{
		prod[i]=rule[i][1];
		delay[i]=rule[i][2];
	}

for(i=0;i<n;i++)
	{
		dv[i]=0;
		tv[i]=0;
	}
for(i=0;i<n;i++)
{
change[i]=0;
}

for(i=0;i<n;i++)
{
printf("%d\t",conf[i]);
}
printf("\n");


//computation starts


do{
c=0;
t=0;
for(i=0;i<n;i++)
	if (dv[i]>0)
	{
		dv[i]=dv[i]-1;
	}

for(i=0;i<n;i++)
	if (tv[i]>0)
	{
		tv[i]=tv[i]-1;		
	}



for(i=0;i<n;i++)
	if ((dv[i]==0)&&(rule[i][0]<=conf[i]))
	{
		sv[i]=1;	//obtain spiking vector
	}
	else
	{ sv[i]=0;}
for(i=0;i<n;i++)
{
if(sv[i]==1)
{c=c+1;}
}



if(c>0)
{
for(j=0;j<n;j++)
	{
		temp[0][j]=0;
		for(k=0;k<n;k++)
		{
		temp[0][j]=temp[0][j]+sv[k]*cons[k][j];
		}
	}

for(i=0;i<n;i++)
	{
	conf[i]=conf[i]+temp[0][i];
	change[i]+=-temp[0][i];
	}
}




for(i=0;i<n;i++)		//checking transfer vector
{
	if(tv[i]==1)
	{
		t=t+1;
		for(b=0;b<n;b++)
		{
			if(syn[i][b]==1)
			{
				if(dv[b]==0)
				{
					if(change[i]>=prod[i])
					{
					conf[b]=conf[b]+prod[i];
					change[i]-=prod[i];
					}
					
				}
			}
			
		}
	}
	
}


printf("\n\n\n\n");
for(i=0;i<n;i++)
{
	printf("%d\t",conf[i]);		//display configurarion
}


for(i=0;i<n;i++)
	{
		if(sv[i]==1)
		{
			if(delay[i]>0)
			{
			dv[i]=delay[i]+1;
			tv[i]=delay[i]+1;
			//l++;
			}
			else
			{
			dv[i]=0;
			tv[i]=0;
			}
		}
		
	}


}while(halt());
}



