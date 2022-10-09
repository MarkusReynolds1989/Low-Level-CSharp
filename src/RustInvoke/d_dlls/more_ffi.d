import core.sys.windows.windows;
import core.sys.windows.dll;

mixin SimpleDllMain;
extern (C):
export @nogc pure int min(const int* input, size_t length)
{
	int result = 2_147_483_647;

	for (size_t i = 0; i < length; i += 1)
	{
		if (input[i] < result)
		{
			result = input[i];
		}
	}

	return result;
}
