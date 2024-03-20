import { BaseDomainVM } from "../../catalog/Models/BaseDomainVM"

export class UserVocabularyVM extends BaseDomainVM {
    wordId?: string
    word?: string
    userId?: string
    user?: string
    pronunciation?: string
    sentence?: string
    vocabDifficultyLevelId?: number = 0
    difficultyLevel?: string
    novelId?: number = 0
    novel?: string
    chapterId?: number = 0
    chapters?: string
    comments?: string
    isNeedHelp: boolean
}