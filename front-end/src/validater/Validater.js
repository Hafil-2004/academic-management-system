export const Validater = {
  isEmail: (email) => {
    if (email == null) {
      return false;
    }
    const re =
      /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email.trim()).toLowerCase());
  },
  isPhone: (phone) => {
    if (phone == null) {
      return false;
    }
    const re = /^(03|07|08|09|01[2|6|8|9])+([0-9]{8})$/g;
    return re.test(String(phone.trim()));
  },
  isNumber: (number) => {
    if (number == null) {
      return false;
    }
    const re = /^\d+$/;
    return re.test(String(number.trim()));
  },
  isCitizenId: (citizenId) => {
    if (citizenId == null) {
      return false;
    }
    const re = /^(\d{9}|\d{12})$/;
    return re.test(String(citizenId.trim()));
  },
  isTaxCode: (taxCode) => {
    if (taxCode == null) {
      return false;
    }
    const re = /^\d{10}$/;
    return re.test(String(taxCode.trim()));
  },
  isContaintSpecialCharacterForName: (str) => {
    console.log(str);
    if (str == null) {
      return false;
    }
    const re = /[~`!#$%\^&*+=\-\[\]\\;,./{}|\\":<>\?]/;
    return re.test(String(str.trim()));
  },
  isContaintSpecialCharacter: (str) => {
    console.log(str);
    if (str == null) {
      return false;
    }
    const re = /[~`!#$%\^&*+=\-\[\]\\';,./{}|\\":<>\?]/;
    return re.test(String(str.trim()));
  },
};
