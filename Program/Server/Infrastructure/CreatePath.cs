using static System.Environment;

namespace Infrastructure;

internal static class CreatePath
{
	/// <summary>
	/// Создает директорию с указанным именем в указанной специальной папке.
	/// </summary>
	/// <remarks>Если указанная директория не существует, она будет создана. Этот метод гарантирует, что
	/// директория будет создана в месте, подходящем для хранения данных приложения.</remarks>
	/// <param name="directoryName">Имя директории, которую нужно создать в специальной папке. Этот параметр не может быть пустым или нулевым.</param>
	/// <param name="specialFolder">Специальная папка, в которой нужно создать директорию. По умолчанию используется папка данных приложения, если не указано иное.</param>
	/// <returns>Полный путь созданной директории. Если директория уже существует, возвращает путь существующей директории.</returns>
	private static string CreateDirectoryInSpecialFolder(
		string directoryName,
		SpecialFolder specialFolder = SpecialFolder.ApplicationData
		)
	{
		string path = Path.Combine(GetFolderPath(specialFolder), directoryName);
		DirectoryInfo? directoryInfo = new(path); // Инициализируем объект класса для создания директории
		if (!directoryInfo.Exists) Directory.CreateDirectory(path); // Если директория не существует, то мы её создаём по пути fullPath
		return path;
	}
	/// <summary>
	/// Создает директорию в указанной специальной папке, опционально включая множество вложенных директорий,
	/// определенных предоставленными именами.
	/// </summary>
	/// <remarks>Этот метод позволяет создавать вложенные директории, указав несколько имен в параметре
	/// 'directoryNames'. Если указанная специальная папка не существует, она будет создана при необходимости.</remarks>
	/// <param name="specialFolder">Специальная папка, в которой нужно создать директорию. По умолчанию используется папка данных приложения, если не указано иное.</param>
	/// <param name="directoryNames">Массив имен директорий, которые нужно создать в специальной папке. Должно быть предоставлено хотя бы одно имя директории.</param>
	/// <returns>Полный путь созданной директории, включая все указанные вложенные директории.</returns>
	/// <exception cref="ArgumentException">Выбрасывается, если не предоставлены имена директорий.</exception>
	private static string CreateDirectoryInSpecialFolder(
		SpecialFolder specialFolder = SpecialFolder.ApplicationData,
		params string[] directoryNames
		)
	{
		string directory = string.Empty;
		string path = string.Empty;
		bool firstCycle = true;
		if (directoryNames.Length == 0)
		{
			throw new ArgumentException();
		}
		foreach (string partsOfThePath in directoryNames)
		{
			directory = firstCycle
				? partsOfThePath
				: Path.Combine(directory, partsOfThePath);
			firstCycle = firstCycle && false;
			path = CreateDirectoryInSpecialFolder(
				directoryName: directory, specialFolder: specialFolder);
		}
		return path;
	}
	/// <summary>
	/// Создает полный путь к файлу в указанной специальной папке, опционально включая дополнительные подпапки.
	/// </summary>
	/// <remarks>Все необходимые подпапки в специальной папке будут созданы перед возвратом пути к файлу.</remarks>
	/// <param name="fileName">Имя файла, для которого нужно сгенерировать путь. Не может быть пустым или нулевым.</param>
	/// <param name="specialFolder">Специальная папка, в которой нужно создать путь к файлу. По умолчанию используется папка данных приложения, если не указано иное.</param>
	/// <param name="directory">Опциональный массив имен подпапок, которые нужно включить в путь. Эти директории будут созданы в
	/// указанной специальной папке, если они не существуют.</param>
	/// <returns>Строка, содержащая полный путь к указанному файлу в назначенной специальной папке и подпапках.</returns>
	private static string CreatePathToFileInSpecialFolder(
		string fileName,
		SpecialFolder specialFolder = SpecialFolder.ApplicationData,
		params string[] directory)
	{
		string directoryPath = CreateDirectoryInSpecialFolder(
			directoryNames: directory, specialFolder: specialFolder);
		return Path.Combine(directoryPath, fileName);
	}
}
