#include<stdio.h>
#include<string.h>
int ana (char[],char[]);
main()
{
	char a[20],b[20];
	int f;
	gets(a);
	gets(b);
	f=ana(a,b);
	if(f==1)
	{
		printf("can\n");
	}
	else
	{
		printf("cant\n");
	}
}
int ana(char x[],char y[])
{
	int first[26]={0},second[26]={0},c=0,k;
	while (x[c]!='\0')
	{
		first[x[c]-'a']++;
		k=x[c]-'a';
		printf("%d\n",k );
		c++;
	}
	c=0;
	while (y[c]!='\0')
	{
		second[y[c]-'a']++;
		c++;
	}
	for(c=0;c<26;c++)
	{
		if(first[c]!=second[c])
			return 0;
	}
	return 1;
}
