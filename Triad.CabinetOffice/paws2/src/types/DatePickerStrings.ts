import { IDatePickerStrings } from 'office-ui-fabric-react/lib/DatePicker';

export class DatePickerStrings implements IDatePickerStrings {
    public months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    public shortMonths = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    public days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    public shortDays = ['S', 'M', 'T', 'W', 'T', 'F', 'S'];
    public goToToday = 'Go to today';
    public isRequiredErrorMessage = '';
}