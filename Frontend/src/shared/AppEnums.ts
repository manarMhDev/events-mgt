import { FormType, InvitationStatus, Language, SeatingChart } from "./api/service-proxies";
export enum Prefixes {
    A = 1,
    B,C,
    D,E,
    F,G,
    H,I,
    J,K,
    L,M,
    N,O,
    P,Q,
    R,S,
    T,U,
    V,W,
    X,Y,
    Z
}
export class AppPrefixesType {
    static getName(val: Prefixes) {
        switch (val) {
            case Prefixes.A:
                return 'A';
                case Prefixes.B:
                    return 'B';
                    case Prefixes.C:
                        return 'C';
                        case Prefixes.D:
                            return 'D';
                            case Prefixes.E:
                                return 'E';
                                case Prefixes.F:
                                    return 'F';
                                    case Prefixes.G:
                                        return 'G';
                                        case Prefixes.H:
                                            return 'H';
                                            case Prefixes.I:
                                                return 'I';
                                                case Prefixes.J:
                                                    return 'J';
                                                    case Prefixes.K:
                                                        return 'K';
                                                        case Prefixes.L:
                                                            return 'L';
                                                            case Prefixes.M:
                                                                return 'M';
                                                                case Prefixes.N:
                                                                    return 'N';
                                                                    case Prefixes.O:
                                                                        return 'O';
                                                                        case Prefixes.P:
                                                                            return 'P';
                                                                            case Prefixes.Q:
                                                                                return 'Q';
                                                                                case Prefixes.R:
                                                                                    return 'R';
                                                                                    case Prefixes.S:
                                                                                        return 'S';
                                                                                        case Prefixes.T:
                                                                                            return 'T';
                                                                                            case Prefixes.U:
                                                                                                return 'U';
                                                                                                case Prefixes.V:
                                                                                                    return 'V';
                                                                                                    case Prefixes.X:
                                                                                                        return 'X';
                                                                                                        case Prefixes.Y:
                                                                                                            return 'Y';
                                                                                                            case Prefixes.Z:
                                                                                                                return 'Z';
            default:
               return '';
        }
    }
}
export class AppInvitationStatus {
    static getName(val: InvitationStatus) {
        switch (val) {
            case InvitationStatus.Apologized:
                return 'تم الاعتذار';
                case InvitationStatus.Confirmed:
                    return 'تم التأكيد';
                    case InvitationStatus.Sent:
                        return 'تم الإرسال';
                        case InvitationStatus.UnderConsideration:
                            return 'قيد الدراسة';
            default:
               return '';
        }
    }
}
export class AppFormType {
    static getName(val: FormType) {
        switch (val) {
            case FormType.External:
                return 'خارجي';
                case FormType.Internal:
                    return 'داخلي';
            default:
               return '';
        }
    }
}


export class AppLanguageType {
    static getName(val: Language) {
        switch (val) {
            case Language.Arabic:
                return 'عربي';
                case Language.English:
                    return 'انجليزي';
            default:
               return '';
        }
    }
}

export class AppLanguageClass{
    key : string;
    value : number;
    constructor(k,v){
        this.key = k;
        this.value = v;
    }
}

export class AppSeatingType {
    static getName(val: SeatingChart) {
        switch (val) {
            case SeatingChart.ColsRows:
                return 'أعمدة وصفوف';
                case SeatingChart.Circular:
                    return 'دائري';
            default:
               return '';
        }
    }
}

export class AppSeatingClass{
    key : string;
    value : number;
    constructor(k,v){
        this.key = k;
        this.value = v;
    }

    
}






