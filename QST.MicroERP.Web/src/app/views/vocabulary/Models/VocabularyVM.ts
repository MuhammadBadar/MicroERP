import { BaseDomainVM } from "../../catalog/Models/BaseDomainVM"
import { UserVocabularyVM } from "./UserVocabularyVM"

export class VocabularyVM extends BaseDomainVM {
    word?: string
    englishMeaning?: string
    urduMeaning?: string
    userVocab: UserVocabularyVM
}
