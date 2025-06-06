"C:\Users\Administrator\Downloads\grouse-main\grouse-main\DesktopGoose v0.31\GooseDesktop.exe"

string createCmd = $"/Create /TN \"ShowMyApp\" /TR \"{exePath}\" " +
                   $"/SC ONCE /ST {startTime} /RL HIGHEST /F /RU \"{username}\"";
string createCmd = $"/Create /TN \"ShowMyApp\" /TR \"\\\"{exePath}\\\"\" /SC ONCE /ST {startTime} /RL HIGHEST /F /RU \"{username}\"";
C:\Windows\Help\Windows\IndexStore\en-US
