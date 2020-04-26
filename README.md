# audio-reminder
![Project diagram](https://github.com/dupop/audio-reminder/blob/develop/Project%20component%20diagram.png)

Project main features status:
- [x] Components (projects) created - win service, class lib and win forms apps
- [x] Communication between components - contracts, WCF, pipes, starting processes
- [x] Detailed logging - Serilog with custom exception logging
- [x] UI basic functionallity - reminder CRUD operations, changing settings, beeing notified of events and dismissing them
- [ ] UI specific details - calender view, sound choosing and custom sound playing
- [x] Persistence of settings and reminders - data model, xml serialization and sotrage/retrival of entity lists from xml files
- [x] Reminder scheduler - heart of the app, keeps track of next reminder to be fired
- [ ] Installer, Uninstaller, Updater
- [ ] Translation config system
