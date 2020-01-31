#include <stdlib.h>
#include <stdio.h>
#include <string.h>

void* TransformToCharArray (const char* string)
{
	size_t	length = 0;
    char*	result = NULL;
    
	if (string == NULL)
		return NULL;

	length = strlen(string);
	if (length == 0)
		return NULL;
		
    result = (char*)malloc(length + 1);
	if (result == NULL)
		return NULL;

	strncpy(result, string, length);
	result[length] = 0;

	return (void*)result;
}

void TransformFreeCharArray (void* string)
{
	/* DEBUG
	if (string != NULL)
		printf ("TransformFreeCharArray: %s\n", string);
	*/
	free (string);
}

void* TransformToArrayOfCharArray (const char* string, const char delimiter)
{
	size_t	index = 0;
	size_t	count = 0;
	size_t	length = 0;
    char*	buffer = NULL;
	char**	result = NULL;
	char*	token  = NULL;
	char*	delim  = NULL;
	
	if (string == NULL)
		return NULL;

	length = strlen(string);
	if (length == 0)
		return NULL;
	
	// Need a buffer to prevent manipulations of 'const char*' by 'strtok ()'.
    buffer = (char*)malloc(length + 1);
	if (buffer == NULL)
		return NULL;

	strncpy(buffer, string, length);
	buffer[length] = 0;

	// Prepare the delimiter string, 'strtok ()' requires.
	delim = (char*)malloc(2);
	if (delim == NULL)
	{
		free (buffer);
		return NULL;
	}

	delim[0] = delimiter;
	delim[1] = 0;
	
	// Count the tokens segments.
	count = 1;
	for (index = 0; index < length; index++)
	{
		if (buffer[index] == delimiter)
			count++;
	}
	/* DEBUG
	printf ("TransformToArrayOfCharArray: Number of sub-strings:%d\n", count);
	*/
		
	// Allocate memory to store an array of char array. Terminate the array with NULL and every char array with \0.
	result = (char**)calloc(sizeof(char*), count + 1);
	if (result == NULL)
	{
		free (buffer);
		return NULL;
	}
	
	token = strtok(buffer, delim);
	
	index = 0;
	while (token != NULL)
	{
		length = strlen(token);
		if (length == 0)
		{
			// Empty token must be repersented as zero length string.
			result[index] = (char*)malloc(1);
			result[index] = 0;
		}
		else
		{
			result[index] = (char*)malloc(length + 1);
			if (result[index] != NULL)
			{
				strncpy(result[index], token, length);
				result[index][length] = 0;
			}
		}		
		/* DEBUG
		printf ("TransformToArrayOfCharArray: Index:%d Sub-string:%s\n", index, result[index]);
		*/

		index++;
		if (index >= count)
			break;
		token = strtok(NULL, delim);
	}

	free (delim);
	free (buffer);

	return (void*)result;
}

void TransformFreeArrayOfCharArray (void* array)
{
	char** buffer = (char**)array;
	size_t count  = 0;

	while (buffer[count] != NULL)
	{
		/* DEBUG
		printf ("TransformFreeArrayOfCharArray: Index:%d Sub-string:%s\n", count, buffer[count]);
		*/

		free (buffer[count]);
		count++;
	}
	
	free (buffer);
}

long __ParseLong (char** string)
{
	char* substr  = *string;
	long  number  = 0;
	long  reverse = 1;
	
	// Skip all leading characters, that are not ['0'-'9'], '.' and '\0'.
	while (*substr != 0 && !isdigit(*substr) && *substr != '.')
	{
		if (*substr == '-')
			reverse = -reverse;
		substr++;
	}
	
	for (number = 0; isdigit(*substr); substr++)
	    number = 10 * number + (*substr - '0');

	*string = substr;
	
	return number * reverse;
}

void* TransformToArrayOfByte (const char* string, unsigned int* count)
{
	char*	substring = (char*)string;
	size_t	index     = 0;
	char*	result    = NULL;
	long	value     = 0;
	
	*count = 0;
	while (*substring != 0)
	{
		__ParseLong (&substring);
		(*count)++;
	}
	
	if (*count == 0)
		return NULL;
	
	result = (char*)calloc (sizeof(char), *count + 1);
	
	substring = (char*)string;
	while (*substring != 0)
	{
		value = __ParseLong (&substring);
		result[index] = (char)value;
		/* DEBUG
		printf ("TransformToArrayOfByte: Index:%d Value:%d\n", index, result[index]);
		*/

		index++;		
		if (index >= *count)
			break;
	}
	return result;
}

void TransformFreeArrayOfByte (void* array)
{
	char* buffer = (char*)array;

	free (buffer);
}

void* TransformToArrayOfInt (const char* string, unsigned int* count)
{
	char*	substring = (char*)string;
	size_t	index     = 0;
	int*	result    = NULL;
	long	value     = 0;
	
	*count = 0;
	while (*substring != 0)
	{
		__ParseLong (&substring);
		(*count)++;
	}
	
	if (*count == 0)
		return NULL;
	
	result = (int*)calloc (sizeof(int), *count + 1);
	
	substring = (char*)string;
	while (*substring != 0)
	{
		value = __ParseLong (&substring);
		result[index] = (int)value;
		/* DEBUG
		printf ("TransformToArrayOfInt: Index:%d Value:%d\n", index, result[index]);
		*/

		index++;		
		if (index >= *count)
			break;
	}
	return result;
}

void TransformFreeArrayOfInt (void* array)
{
	int* buffer = (int*)array;

	free (buffer);
}