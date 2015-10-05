#include<stdio.h>
main()
{
	int temp,n,sum=0,r,s;
int fact(int);
	printf("enter num");
	scanf("%d",&n);
	temp=n;
	while(n)
	{
		r=n%10;
		s=fact(r);
		sum+=s;
		n=n/10;
	}
	printf("%d",sum);
	if(sum==temp)
	{
		printf("strong\n");

	}
}
	int fact(int a)
	{
		int k=1,i;
		if(a==0||a==1)
		{
			return 1;
		}
		else
		{
			k=a*fact(a-1);
			/*for(i=1;i<=a;i++)
			{
				k=(k*i);
			}*/
			return k;

		}
	}

